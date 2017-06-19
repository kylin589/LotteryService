using Lottery.Entities;
using LotteryService.Common.Dependency;

namespace LotteryService.Application.Lottery
{
    public interface ILotteryPredictDataAppService : ITransientDependency
    {
        LotteryPredictData GetCurrentLotteryPredictData(string normId, int currentPredictPeriod);

        bool Update(LotteryPredictData lotteryPredictData);

        bool Insert(LotteryPredictData lotteryPredictData);
    }
}