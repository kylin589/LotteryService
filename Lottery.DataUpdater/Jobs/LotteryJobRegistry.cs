using System.Linq;
using FluentScheduler;
using Microsoft.Practices.ServiceLocation;

namespace Lottery.DataUpdater.Jobs
{
    public class LotteryJobRegistry : Registry
    {
        public LotteryJobRegistry()
        {
            var lotteryeConfigLoader = ServiceLocator.Current.GetInstance<ILotteryUpdateConfigLoader>();
            var lotteryConfigs = lotteryeConfigLoader.GetLotteryUpdateConfigs();

            var bjpksConfig = lotteryConfigs.First(p => p.Name == "bjpks");
            //Schedule<BjpksLotteryDataJob>().ToRunNow().AndEvery(bjpksConfig.Interval).Seconds();
            

        }
    }
}