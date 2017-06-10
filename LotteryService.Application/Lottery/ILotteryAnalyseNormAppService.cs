using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common.Dependency;
using LotteryService.Common.Enums;

namespace LotteryService.Application.Lottery
{
    public interface ILotteryAnalyseNormAppService : ITransientDependency
    {
        IDictionary<LotteryType, IList<LotteryAnalyseNorm>> GetAll();
    }
}