using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common.Dependency;
using LotteryService.Domain.Interfaces.Repository.Dapper;
using LotteryService.Domain.Interfaces.Service;

namespace LotteryService.Domain.Lottery
{
    public class LotteryConfigService : ILotteryConfigService
    {
        private ILotteryConfigDapperRepostory _lotteryConfigDapperRepostory;

        public LotteryConfigService(ILotteryConfigDapperRepostory lotteryConfigDapperRepostory)
        {
            _lotteryConfigDapperRepostory = lotteryConfigDapperRepostory;
        }

        public string GetLotteryConfig(string lotteryType)
        {
            return _lotteryConfigDapperRepostory.GetLotteryConfig(lotteryType);
        }

        public IDictionary<string, string> GetLotteryConfigs()
        {
            return _lotteryConfigDapperRepostory.GetLotteryConfigs();
        }
    }
}