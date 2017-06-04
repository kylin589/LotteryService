using Microsoft.Practices.ServiceLocation;
using Quartz;
using Quartz.Impl;
using System.Linq;

namespace Lottery.DataUpdater.Jobs
{
    public class JobScheduler
    {
        public static void Start()
        {
            var lotteryeConfigLoader = ServiceLocator.Current.GetInstance<ILotteryUpdateConfigLoader>();
            var lotteryConfigs = lotteryeConfigLoader.GetLotteryUpdateConfigs();

            var bjpksConfig = lotteryConfigs.First(p => p.Name == "bjpks");

            //启动Scheduler，只需执行一次
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            //创建指定的时间触发器一次的触发器
            var trigger = TriggerBuilder.Create()
                .StartNow()
                .WithIdentity("CrawlLotteryDataJop", "LotteryGroup")
                .WithSimpleSchedule(s => s.WithIntervalInSeconds(bjpksConfig.Interval).RepeatForever())
                .Build();

            //添加任务
            scheduler.ScheduleJob(JobBuilder.Create<BjpksLotteryDataJob>().Build(), trigger);
            //scheduler.ScheduleJob(JobBuilder.Create<AnotherJob>().Build(), anotherTrigger);
        }
    }
}