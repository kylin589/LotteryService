using System.IO;
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
            AutofacBootstrap.CoreAutoFacInit();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            JobConfig.JobRegister();
            AutoMapperConfig.RegisterMappings();

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(Server.MapPath("log4net.config")));
           
        }

        //protected void Application_EndRequest()
        //{
        //    var contextManager = ServiceLocator.Current.GetInstance<IContextManager<LotteryDbContext>>();
        //    if (contextManager != null)
        //    {
        //        contextManager.GetContext().Dispose();
        //    }
        //}
    }
}
