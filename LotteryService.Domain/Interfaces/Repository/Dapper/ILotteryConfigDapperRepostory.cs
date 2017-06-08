using System.Collections.Generic;
using LotteryService.Common.Dependency;

namespace LotteryService.Domain.Interfaces.Repository.Dapper
{
    public interface ILotteryConfigDapperRepostory : ITransientDependency
    {
        string GetLotteryConfig(string lotteryType);

        IDictionary<string, string> GetLotteryConfigs();
    }
}