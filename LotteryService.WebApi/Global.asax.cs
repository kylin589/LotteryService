using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Lottery.DataUpdater.Jobs;
using LotteryService.Common.Enums;
using LotteryService.Common.Tools;
using LotteryService.Data.Context;
using LotteryService.Data.Context.Interfaces;
using LotteryService.Domain.Logs;
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
            //            JobConfig.JobRegister();

            JobScheduler.Start();
            AutoMapperConfig.RegisterMappings();

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(Server.MapPath("log4net.config")));
           
        }

        protected void Application_EndRequest()
        {
            var contextManager = ServiceLocator.Current.GetInstance<IContextManager<LotteryDbContext>>();
            if (contextManager != null)
            {
                contextManager.GetContext().Dispose();
            }
        }

        protected void Application_End()
        {
            LogDbHelper.LogWarn("应用停止，IIS将应用回收", "Global => Application_End",OperationType.Other);

            Thread.Sleep(1000);

            try
            {
                //这里设置你的web地址，可以随便指向你的任意一个aspx页面甚至不存在的页面，目的是要激发Application_Start  
                string url = Utils.GetConfigValuesByKey("EndAppTriggerRestartUrl");
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                Stream receiveStream = myHttpWebResponse.GetResponseStream();//得到回写的字节流  
                LogDbHelper.LogWarn("App Restart Success", "Global => Application_End", OperationType.Other);
            }
            catch (Exception ex)
            {
                LogDbHelper.LogWarn("App Restart Failure" + ex.Message, "Global => Application_End", OperationType.Other);              
            }

        }
    }
}
