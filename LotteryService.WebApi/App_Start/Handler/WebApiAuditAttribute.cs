using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using AutoMapper.Mappers;
using LotteryService.Application.Log;
using LotteryService.Application.Log.Dtos;
using LotteryService.Common;
using LotteryService.Common.Extensions;

namespace LotteryService.WebApi
{
    public class WebApiAuditFilterAttribute : ActionFilterAttribute
    {
        private Stopwatch _stopwatch;
        private string m_auditId = string.Empty;


        public WebApiAuditFilterAttribute()
        {
            _stopwatch = new Stopwatch();
        }

        public override  void OnActionExecuting(HttpActionContext actionContext)
        {
            _stopwatch.Start();

            // Get the request lifetime scope so you can resolve services.
            var requestScope = actionContext.Request.GetDependencyScope();

            // Resolve the service you want to use.
            var auditAppService = requestScope.GetService(typeof(IAuditAppService)) as IAuditAppService;

            var auditLogInput = new AuditLogInput()
            {
                UserId = "tempUserId",
                BrowserInfo = RequestExtend.RequestValue(LsConstant.RequestBrowser),
                ClientName = RequestExtend.RequestValue(LsConstant.RequestClientName),
                ClientIpAddress = RequestExtend.RequestValue(LsConstant.RequestClientAddress),
                MethodName = actionContext.Request.Method.ToString(),
                ApiAddress = actionContext.Request.RequestUri.LocalPath,
                Parameters = GetRequestParamters(actionContext),
                ActionName = actionContext.ActionDescriptor.ActionName,
                ControllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                IsExecSuccess = false,
                ExecutionDuration = 0,

            };
            m_auditId = auditAppService.InsertAuditLog(auditLogInput);

           base.OnActionExecuting(actionContext);
        }

        private string GetRequestParamters(HttpActionContext actionContext)
        {
            var paramters = actionContext.ActionArguments;    

            if (Enumerable.Any(actionContext.ActionDescriptor.GetCustomAttributes<EncryptAuditLogParamsAttribute>()))
            {
                return "************************";
            }

            return paramters.ToJsonString();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            // Get the request lifetime scope so you can resolve services.
            var requestScope = actionExecutedContext.Request.GetDependencyScope();

            // Resolve the service you want to use.
            var auditAppService = requestScope.GetService(typeof(IAuditAppService)) as IAuditAppService;

            var auditLogEdit = new AuditLogEdit()
            {
                Id = m_auditId,            
                IsExecSuccess = true,
            };
            if (actionExecutedContext.Exception != null)
            {
                auditLogEdit.Exception = actionExecutedContext.Exception.ToString();
                auditLogEdit.IsExecSuccess = false;
            }
            _stopwatch.Stop();
            auditLogEdit.ExecutionDuration = _stopwatch.Elapsed.Milliseconds;
            auditAppService.UpdateAuditLog(auditLogEdit);       
            base.OnActionExecuted(actionExecutedContext);
            _stopwatch.Reset();
        }
    }
}