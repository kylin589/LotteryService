using System.Collections.Generic;
using LotteryService.Common.Dependency;
using LotteryService.Common.Enums;

namespace LotteryService.Domain.Interfaces.Service
{
    public interface IAnylseNormService : ITransientDependency
    {
        IList<int> GetUserSelectedPlans(string userId, LotteryType lotteryType);
    }
}