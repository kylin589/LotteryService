using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using LotteryService.Common;
using LotteryService.Common.Enums;
using LotteryService.Common.Tools;
using LotteryService.Domain.Logs;

namespace LotteryService.WebApi
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;

            //全局异常捕获，记录未被捕获的异常
            LogDbHelper.LogFatal(exception,
                actionExecutedContext.Request.RequestUri.OriginalString + "=>" + actionExecutedContext.Request.Method,
                OperationType.Exception
                );

            actionExecutedContext.Response = 
                actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, ResponseUtils.ErrorResult<object>(exception));

            base.OnException(actionExecutedContext);

        }
    }
}