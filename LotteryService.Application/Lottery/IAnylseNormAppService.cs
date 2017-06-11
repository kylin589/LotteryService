using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common.Dependency;
using LotteryService.Common.Enums;

namespace LotteryService.Application.Lottery
{
    public interface IAnylseNormAppService : ITransientDependency
    {
        IList<int> GetUserSelectedPlans(string userId, LotteryType lotteryType);
    }
}