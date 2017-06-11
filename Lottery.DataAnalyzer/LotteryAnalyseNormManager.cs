using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Application.Lottery;
using LotteryService.Common;
using LotteryService.Common.Enums;
using Microsoft.Practices.ServiceLocation;

namespace Lottery.DataAnalyzer
{
    public class LotteryAnalyseNormManager : ILotteryAnalyseNormManager
    {
        private static ILotteryAnalyseNormAppService _analyseNormAppService;

        private static IDictionary<LotteryType,IList<LotteryAnalyseNorm>> _lotteryAnalyseNorms;

        static LotteryAnalyseNormManager()
        {
            _analyseNormAppService = ServiceLocator.Current.GetInstance<ILotteryAnalyseNormAppService>();
            _lotteryAnalyseNorms = _analyseNormAppService.GetAll();

            foreach (var item in _lotteryAnalyseNorms)
            {
                var lotteryAnalyseNorm = string.Format(LsConstant.LotteryAnalyseNorm, item.Key);
                foreach (var anlyse in item.Value)
                {
                    RedisHelper.SetHash(lotteryAnalyseNorm, anlyse.Id,anlyse);
                }
            }
        }
    

        public ICollection<LotteryAnalyseNorm> LoadLotteryAnalyseNorms(LotteryType lotteryType)
        {
            var lotteryAnalyseNorm = string.Format(LsConstant.LotteryAnalyseNorm, lotteryType);

            return RedisHelper.GetAll<LotteryAnalyseNorm>(lotteryAnalyseNorm);
        }

        public bool AddLotteryAnalyseNorms(LotteryType lotteryType, LotteryAnalyseNorm lotteryAnalyseNorm)
        {
            return true;
        }

        public bool RemoveLotteryAnalyseNorms(LotteryType lotteryType, LotteryAnalyseNorm lotteryAnalyseNorm)
        {           
            return true;
        }
    }
}