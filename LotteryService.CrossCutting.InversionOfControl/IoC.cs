using System;
using System.Linq;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Lottery.Entities;
using Lottery.Entities.Extend;
using LotteryService.Common.Dependency;
using LotteryService.Data.Context;
using LotteryService.Data.Context.Interfaces;
using LotteryService.Data.Repository.Dapper.Account;
using LotteryService.Data.Repository.Dapper.Log;
using LotteryService.Data.Repository.Dapper.Lottery;
using LotteryService.Data.Repository.EntityFramework.Common;
using LotteryService.Domain.Interfaces.Repository.Common;
using LotteryService.Domain.Interfaces.Repository.Dapper;
using LotteryService.Domain.Interfaces.Service.Common;
using LotteryService.Domain.Services.Common;
using Microsoft.Practices.ServiceLocation;

namespace LotteryService.CrossCutting.InversionOfControl
{
    public class IoC
    {
        public ContainerBuilder ContainerBuilder { get; private set; }

        private IContainer _container;

        private readonly object _lockObj = new object();

        public IContainer Container
        {
            get
            {
                if (_container == null)
                {
                    lock (_lockObj)
                    {
                        if (_container == null)
                        {
                            _container = ContainerBuilder.Build();
                        }
                    }
                }
                return _container;
            }
        }

        public IoC()
        {
            ContainerBuilder = new ContainerBuilder();
            SetupResolveTypes();
            SetupResolveRules();
            SetupResolveGenerics();
            SetupResolveReadOnlyRepositories();
            SetupResolveLog4Net();
        }

        private void SetupResolveLog4Net()
        {
            ContainerBuilder.RegisterModule<LoggingModule>();
        }

        private void SetupResolveReadOnlyRepositories()
        {
            ContainerBuilder.RegisterType<LotteryDataDapperRepostory>()
                .As<IDapperRepository<LotteryData>>()
                .InstancePerDependency();

            ContainerBuilder.RegisterType<AuditLogDapperRepostory>()
                .As<IDapperRepository<AuditLog>>()
                .InstancePerDependency();

            //ContainerBuilder.RegisterType<LotteryConfigDapperRepostory>()
            //    .As<IDapperRepository<LotteryFeature>>()
            //    .InstancePerDependency();

            ContainerBuilder.RegisterType<UserDapperRepository>()
                .As<UserDapperRepository>()
                .As<IDapperRepository<User>>()
                .InstancePerDependency();

        }

        private void SetupResolveGenerics()
        {
            ContainerBuilder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerDependency();

            ContainerBuilder.RegisterGeneric(typeof(Service<>))
                .As(typeof(IService<>))
                .InstancePerDependency();

            ContainerBuilder.RegisterGeneric(typeof(DapperService<>))
                .As(typeof(IDapperService<>))
                .InstancePerDependency();
        }

        private void SetupResolveTypes()
        {
            ContainerBuilder.RegisterType<LotteryDbContext>().As<IDbContext>()
                .InstancePerDependency();
            ContainerBuilder.RegisterGeneric(typeof(ContextManager<>)).As(typeof(IContextManager<>))
                .SingleInstance();
            ContainerBuilder.RegisterGeneric(typeof(UnitOfWork<>)).As(typeof(IUnitOfWork<>));

        }

        private void SetupResolveRules()
        {
            var transientType = typeof(ITransientDependency);
            var singletonType = typeof(ISingletonDependency);

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(p => p.FullName.Contains(Constant.LotterySchema)).ToArray();

            ContainerBuilder.RegisterAssemblyTypes(assemblies)
                .Where(p => transientType.IsAssignableFrom(p) && p != transientType)
                .AsImplementedInterfaces()
                .AsSelf()
                .InstancePerDependency();

            ContainerBuilder.RegisterAssemblyTypes(assemblies)
               .Where(p => singletonType.IsAssignableFrom(p) && p != singletonType)
               .AsImplementedInterfaces()
               .AsSelf()
               .SingleInstance();

            //ContainerBuilder.RegisterAssemblyTypes(assemblies)
            //    .Where(p => p.BaseType == entityType && p != entityType)
            //    .AsSelf()
            //    .InstancePerDependency();

        }

        public void SetServiceLocator(IContainer iocContainer)
        {
            // Set the service locator to an AutofacServiceLocator.
            var csl = new AutofacServiceLocator(iocContainer);
            ServiceLocator.SetLocatorProvider(() => csl);
        }
    }
}
