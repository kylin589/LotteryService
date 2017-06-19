using Lottery.Entities;
using LotteryService.Domain.Interfaces.Service;

namespace LotteryService.Application.Lottery
{
    public class LotteryPredictDataAppService : ILotteryPredictDataAppService
    {
        private readonly ILotteryPredictDataService _lotteryPredictDataService;

        public LotteryPredictDataAppService(ILotteryPredictDataService lotteryPredictDataService)
        {
            _lotteryPredictDataService = lotteryPredictDataService;
        }

        public LotteryPredictData GetCurrentLotteryPredictData(string normId, int currentPredictPeriod)
        {
            return _lotteryPredictDataService.GetCurrentLotteryPredictData(normId, currentPredictPeriod);
        }

        public bool Update(LotteryPredictData lotteryPredictData)
        {
            return _lotteryPredictDataService.Update(lotteryPredictData);
        }

        public bool Insert(LotteryPredictData lotteryPredictData)
        {
            return _lotteryPredictDataService.Insert(lotteryPredictData);
        }
    }
}