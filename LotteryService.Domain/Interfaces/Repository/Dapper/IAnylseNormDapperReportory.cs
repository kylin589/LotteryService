using System.Collections.Generic;
using LotteryService.Common.Dependency;
using LotteryService.Common.Enums;
using Lottery.Entities;

namespace LotteryService.Domain.Interfaces.Repository.Dapper
{
    public interface IAnylseNormDapperReportory : ITransientDependency
    {
        IList<int> GetUserSelectedPlans(string userId, LotteryType lotteryType,out bool isSysDefault);

        void InsertUserPlans(string userId, LotteryType lotteryType, UserBasicNorm userBasicNorm, IList<int> planIds);

        void UpdateUserPlans(string userId, LotteryType lotteryType, UserBasicNorm userBasicNorm, IList<int> planIds, IList<int> userOldLotteryPlanIds);
    }
}