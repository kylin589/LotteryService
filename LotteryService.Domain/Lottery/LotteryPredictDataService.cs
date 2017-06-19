using Lottery.Entities;
using LotteryService.Domain.Interfaces.Repository.Dapper;
using LotteryService.Domain.Interfaces.Service;

namespace LotteryService.Domain.Lottery
{
    public class LotteryPredictDataService : ILotteryPredictDataService
    {
        private readonly ILotteryPredictDataDapperRepostory _lotteryPredictDataDapperRepostory;

        public LotteryPredictDataService(ILotteryPredictDataDapperRepostory lotteryPredictDataDapperRepostory)
        {
            _lotteryPredictDataDapperRepostory = lotteryPredictDataDapperRepostory;
        }

        public LotteryPredictData GetCurrentLotteryPredictData(string normId, int currentPredictPeriod)
        {
            return _lotteryPredictDataDapperRepostory.GetCurrentLotteryPredictData(normId,currentPredictPeriod);
        }

        public bool Update(LotteryPredictData lotteryPredictData)
        {
            return _lotteryPredictDataDapperRepostory.Update(lotteryPredictData);
        }

        public bool Insert(LotteryPredictData lotteryPredictData)
        {
            return _lotteryPredictDataDapperRepostory.Insert(lotteryPredictData);
        }
    }
}