using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common.Dependency;
using LotteryService.Common.Enums;

namespace Lottery.DataAnalyzer
{
    public interface ILotteryAnalyseNormManager : ISingletonDependency
    {
        ICollection<LotteryAnalyseNorm> LoadLotteryAnalyseNorms(LotteryType lotteryType);

        bool AddLotteryAnalyseNorms(LotteryType lotteryType, LotteryAnalyseNorm lotteryAnalyseNorm);

        bool RemoveLotteryAnalyseNorms(LotteryType lotteryType, LotteryAnalyseNorm lotteryAnalyseNorm);

        
    }
}