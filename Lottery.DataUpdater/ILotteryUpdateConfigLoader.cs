using System.Collections.Generic;
using Lottery.DataUpdater.Models;
using LotteryService.Common.Dependency;

namespace Lottery.DataUpdater
{
    public interface ILotteryUpdateConfigLoader : ITransientDependency
    {
        IList<LotteryUpdateConfig> GetLotteryUpdateConfigs();
    }
}