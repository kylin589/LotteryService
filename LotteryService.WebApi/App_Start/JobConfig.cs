using FluentScheduler;
using Lottery.DataUpdater.Jobs;

namespace LotteryService.WebApi
{
    public class JobConfig
    {
        public static void JobRegister()
        {
            JobManager.JobFactory = new StructureMapJobFactory();
            JobManager.Initialize(new LotteryJobRegistry());

        }
    }
}