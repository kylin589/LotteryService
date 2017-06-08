using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common.Dependency;

namespace LotteryService.Application.Lottery
{
    public interface ILotteryConfigAppService : ITransientDependency
    {
        string GetLotteryConfig(string lotteryType);

        IDictionary<string, string> GetLotteryConfigs();
    }
}