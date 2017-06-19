using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common.Dependency;
using LotteryService.Common.Enums;

namespace Lottery.DataAnalyzer
{
    public interface ILotteryDataManager : ISingletonDependency
    {
        IList<LotteryData> GetHistoryLotteryDatas(LotteryType lotteryType);

        IList<LotteryData> GetHistoryLotteryDatas(LotteryType lotteryType, int count);


        LotteryData GetLastLotteryDatas(LotteryType lotteryType);
    }
}