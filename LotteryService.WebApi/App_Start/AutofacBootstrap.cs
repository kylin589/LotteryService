using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using LotteryService.Application.Lottery;
using LotteryService.CrossCutting.InversionOfControl;

namespace LotteryService.WebApi
{
    public class AutofacBootstrap
    {
        public static void CoreAutoFacInit()
        {
            var ioc = new IoC();
            var builder = ioc.ContainerBuilder;

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterWebApiFilterProvider(config);

            // Set the dependency resolver to be Autofac.
            var container = ioc.Container;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            // DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            
            // Set the service locator to an AutofacServiceLocator.
            ioc.SetServiceLocator(ioc.Container);

         

        }

        


    }
}