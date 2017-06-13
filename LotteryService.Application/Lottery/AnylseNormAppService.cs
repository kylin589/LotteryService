using System;
using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common.Enums;
using LotteryService.Common.Excetions;
using LotteryService.Domain.Interfaces.Service;

namespace LotteryService.Application.Lottery
{
    public class AnylseNormAppService : IAnylseNormAppService
    {
        private readonly IAnylseNormService _anylseNormService;

        public AnylseNormAppService(IAnylseNormService anylseNormService)
        {
            _anylseNormService = anylseNormService;
        }

        public IList<int> GetUserSelectedPlans(string userId, LotteryType lotteryType)
        {
            bool isSysDefault;
            return _anylseNormService.GetUserSelectedPlans(userId, lotteryType,out isSysDefault);
        }

        public bool UpdateUserLotteryPlan(string userId, LotteryType lotteryType, IList<int> planIds)
        {
            if (planIds.Count <=0)
            {
                throw new LSException("您还没选中任何计划");
            }
            return _anylseNormService.UpdateUserLotteryPlan(userId, lotteryType, planIds);
        }
    }
}