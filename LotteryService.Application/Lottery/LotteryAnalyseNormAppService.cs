using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common.Enums;
using LotteryService.Domain.Interfaces.Service;

namespace LotteryService.Application.Lottery
{
    public class LotteryAnalyseNormAppService : ILotteryAnalyseNormAppService
    {
        private ILotteryAnalyseNormService _analyseNormService;

        public LotteryAnalyseNormAppService(ILotteryAnalyseNormService analyseNormService)
        {
            _analyseNormService = analyseNormService;
        }

        public IDictionary<LotteryType, IList<LotteryAnalyseNorm>> GetAll()
        {
            return _analyseNormService.GetAll();
        }
    }
}