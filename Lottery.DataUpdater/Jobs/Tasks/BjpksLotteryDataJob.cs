using Lottery.DataUpdater.Jobs;
using LotteryService.Common.Enums;

namespace Lottery.DataUpdater
{
    public class BjpksLotteryDataJob : LotteryDataJob
    {
        public BjpksLotteryDataJob() 
            : base(LotteryType.bjpks)
        {
        }
    }
}