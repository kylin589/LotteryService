using System.Collections.Generic;
using LotteryService.Common.Dependency;
using LotteryService.Common.Enums;

namespace LotteryService.Domain.Interfaces.Repository.Dapper
{
    public interface IAnylseNormDapperReportory : ITransientDependency
    {
        IList<int> GetUserSelectedPlans(string userId, LotteryType lotteryType);
    }
}