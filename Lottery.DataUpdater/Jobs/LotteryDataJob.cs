using System.Linq;
using System.Web.Hosting;
using Autofac;
using FluentScheduler;
using Lottery.DataUpdater.Models;
using Lottery.Entities;
using LotteryService.Application.Lottery;
using LotteryService.CrossCutting.InversionOfControl;
using LotteryService.Data.Repository.EntityFramework.Common;
using LotteryService.Domain.Interfaces.Repository.Common;

namespace Lottery.DataUpdater.Jobs
{
    public class LotteryDataJob : IJob, IRegisteredObject
    {
        private readonly object _lock = new object();

        private bool _shuttingDown;

        public LotteryDataJob()
        {
            HostingEnvironment.RegisterObject(this);
        }

        public void Execute()
        {
            lock (_lock)
            {
                if (_shuttingDown)
                    return;

                var ioc = new IoC();
                var lotteryeConfigLoader = ioc.Container.Resolve<ILotteryUpdateConfigLoader>();
                var lotteryConfig = lotteryeConfigLoader.GetLotteryUpdateConfigs().First(p => p.Name == "bjpks");
                var lotteryRepository = ioc.Container.Resolve<ILotteryDataAppService>();
                //var dataUpdateContainer = new DataUpdateContainer(lotteryConfig, lotteryRepository);
            }
        }

        public void Stop(bool immediate)
        {
            // Locking here will wait for the lock in Execute to be released until this code can continue.
            lock (_lock)
            {
                _shuttingDown = true;
            }

            // HostingEnvironment.UnregisterObject(this);
        }
    }
}