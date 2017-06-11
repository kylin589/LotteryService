using System.Collections.Generic;
using LotteryService.Application.Lottery.Dtos;
using LotteryService.Common;
using LotteryService.Common.Dependency;
using LotteryService.Common.Enums;

namespace Lottery.DataAnalyzer
{
    public interface ILotteryPlanManager : ITransientDependency
    {
        IList<LotteryPlanGroupDto> GetUserLotteryPlans(string userId,LotteryType lotteryType);
    }
}