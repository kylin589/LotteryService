using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common.Dependency;

namespace LotteryService.Domain.Interfaces.Service
{
    public interface ILotteryConfigService : ITransientDependency
    {
        string GetLotteryConfig(string lotteryType);
        IDictionary<string, string> GetLotteryConfigs();
    }
}