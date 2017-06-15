using System;
using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common;
using LotteryService.Domain.Interfaces.Service;

namespace LotteryService.Application.Lottery
{
    public class LotteryConfigAppService : ILotteryConfigAppService
    {
        private ILotteryConfigService _lotteryConfigService;

        public LotteryConfigAppService(ILotteryConfigService lotteryConfigService)
        {
            _lotteryConfigService = lotteryConfigService;
        }

        public string GetLotteryConfig(string lotteryType)
        {
            //if (RedisHelper.KeyExists(string.Format(LsConstant.RedistLotteryFeatureKey, lotteryType)))
            //{
            //    return RedisHelper.Get(string.Format(LsConstant.RedistLotteryFeatureKey, lotteryType));
            //}
            return _lotteryConfigService.GetLotteryConfig(lotteryType);
        }

        public IDictionary<string, string> GetLotteryConfigs()
        {
            return _lotteryConfigService.GetLotteryConfigs();
        }
    }
}