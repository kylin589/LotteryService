using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common.Enums;
using LotteryService.Domain.Interfaces.Repository.Dapper;
using LotteryService.Domain.Interfaces.Service;

namespace LotteryService.Domain.Lottery
{
    public class LotteryAnalyseNormService : ILotteryAnalyseNormService
    {
        private ILotteryAnalyseNormDapperRepostory _analyseNormDapperRepostory;

        public LotteryAnalyseNormService(ILotteryAnalyseNormDapperRepostory analyseNormDapperRepostory)
        {
            _analyseNormDapperRepostory = analyseNormDapperRepostory;
        }

        public IDictionary<LotteryType, IList<LotteryAnalyseNorm>> GetAll()
        {
            return _analyseNormDapperRepostory.GetAll();
        }
    }
}