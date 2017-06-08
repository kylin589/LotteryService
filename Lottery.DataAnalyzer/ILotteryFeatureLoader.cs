using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common.Dependency;
using LotteryService.Common.Enums;

namespace Lottery.DataAnalyzer
{
    public interface ILotteryFeatureLoader : ITransientDependency
    {
        LotteryFeature LoadLotteryFeature(LotteryType lotteryType);

        LotteryPlan GetLotteryPlan(LotteryType lotteryType, int planId);

        IList<LotteryPlan> GetLotteryPlans(LotteryType lotteryType, int groupId);
    }
}