using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Application.Lottery;
using LotteryService.Common.Enums;
using Microsoft.Practices.ServiceLocation;

namespace Lottery.DataAnalyzer
{
    public class LotteryFeatureLoader : ILotteryFeatureLoader
    {
        private readonly ILotteryConfigAppService _lotteryFeatureAppService;

        public LotteryFeatureLoader()
        {
            _lotteryFeatureAppService = ServiceLocator.Current.GetInstance<ILotteryConfigAppService>();
        }

        public LotteryFeature LoadLotteryFeature(LotteryType lotteryType)
        {
            var lotteryConfig = _lotteryFeatureAppService.GetLotteryConfig(lotteryType.ToString());
            return null;
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