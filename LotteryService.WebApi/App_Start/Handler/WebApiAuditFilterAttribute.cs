using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Autofac;
using LotteryService.Application.Log;
using LotteryService.Application.Log.Dtos;
using LotteryService.Application.Lottery;
using LotteryService.Application.Lottery.Dtos;
using LotteryService.Common;
using LotteryService.Common.Extensions;
using LotteryService.CrossCutting.InversionOfControl;
using LotteryService.Data.Context.Interfaces;
using Microsoft.Practices.ServiceLocation;

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
                RequestAddress = actionContext.Request.RequestUri.LocalPath,
                Parameters = actionContext.ActionArguments.ToJsonString(),
                ActionName = actionContext.ActionDescriptor.ActionName,
                ControllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                IsExecSuccess = false,

            };
            m_auditId = auditAppService.InsertAuditLog(auditLogInput);

           base.OnActionExecuting(actionContext);
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