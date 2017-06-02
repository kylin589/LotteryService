using System.Linq;
using Autofac;
using FluentScheduler;
using Lottery.DataUpdater.Models;
using Lottery.Entities;
using LotteryService.CrossCutting.InversionOfControl;

namespace Lottery.DataUpdater.Jobs
{
    public class LotteryJobRegistry : Registry
    {
        public LotteryJobRegistry()
        {
            //var ioc = new IoC();
            //var lotteryeConfigLoader = ioc.Container.Resolve<ILotteryUpdateConfigLoader>();
            //var lotteryConfig = lotteryeConfigLoader.GetLotteryUpdateConfigs().First(p=>p.Name == "bjpks");

            //// Schedule an IJob to run at an interval
            //Schedule<LotteryDataJob>().ToRunNow().AndEvery(lotteryConfig.Interval).Seconds();
        }
    }
}