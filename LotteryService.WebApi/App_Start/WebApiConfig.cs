using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LotteryService.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // 全局异常捕获处理器
            config.Filters.Add(new WebApiExceptionFilterAttribute());

            // 身份认证
            config.Filters.Add(new LsAuthenticationFilter());

            //   审计日志
            config.Filters.Add(new WebApiAuditFilterAttribute());
            
        }
    }
}
