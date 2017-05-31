using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LotteryService.Data.Context;
using LotteryService.Data.Context.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace LotteryService.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoFacBootStrapper.CoreAutoFacInit();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            JobConfig.JobRegister();

        }

        protected void Application_EndRequest()
        {
            var contextManager = ServiceLocator.Current.GetInstance<IContextManager<LotteryDbContext>>() as ContextManager<LotteryDbContext>;
            if (contextManager != null)
            {
                contextManager.GetContext().Dispose();
            }
        }
    }
}
