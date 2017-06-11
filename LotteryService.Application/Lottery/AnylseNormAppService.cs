using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common.Enums;
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
            return _anylseNormService.GetUserSelectedPlans(userId, lotteryType);
        }
    }
}