using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Application.Lottery;
using LotteryService.Common.Enums;
using LotteryService.Common.Extensions;
using LotteryService.Common.Tools;
using Microsoft.Practices.ServiceLocation;

namespace Lottery.DataAnalyzer
{
    public class LotteryFeatureLoader : ILotteryFeatureLoader
    {
        private static IDictionary<LotteryType, LotteryFeature> _lotteryFeatures;

        static LotteryFeatureLoader()
        {
            _lotteryFeatures = new Dictionary<LotteryType, LotteryFeature>();

            var _lotteryFeatureAppService = ServiceLocator.Current.GetInstance<ILotteryConfigAppService>();
            var lotteryConfigs = _lotteryFeatureAppService.GetLotteryConfigs();
            
            foreach (var lc in lotteryConfigs)
            {
                _lotteryFeatures[Utils.StringConvertEnum<LotteryType>(lc.Key)] = lc.Value.ToObject<LotteryFeature>();
            }
        }

        public LotteryFeatureLoader()
        {
          
        }

        public LotteryFeature LoadLotteryFeature(LotteryType lotteryType)
        {
            return _lotteryFeatures[lotteryType];
        }

        public LotteryPlan GetLotteryPlan(LotteryType lotteryType, int planId)
        {
            throw new System.NotImplementedException();
        }

        public IList<LotteryPlan> GetLotteryPlans(LotteryType lotteryType, int groupId)
        {
            throw new System.NotImplementedException();
        }
    }
}