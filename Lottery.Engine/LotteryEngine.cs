using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.DataAnalyzer;
using Lottery.Entities;
using LotteryService.Common.Enums;
using Microsoft.Practices.ServiceLocation;
using LotteryService.Application.Lottery;
using LotteryService.Common.Extensions;
using LotteryService.Common.Tools;

namespace Lottery.Engine
{
    public class LotteryEngine
    {
        private static IDictionary<LotteryType, LotteryFeature> _lotteryFeatures;

        private static IDictionary<LotteryType, LotteryEngine> _lotteryEngines;

        public static LotteryEngine GetLotteryEngine(LotteryType lotteryType)
        {
            return _lotteryEngines[lotteryType];
        }

        public static LotteryFeature GetLotteryFeature(LotteryType lotteryType)
        {
            return _lotteryFeatures[lotteryType];
        }

        static LotteryEngine()
        {
            var _lotteryFeatureLoader = ServiceLocator.Current.GetInstance<ILotteryConfigAppService>();

            var lotteryConfigDataDic = _lotteryFeatureLoader.GetLotteryConfigs();
            _lotteryFeatures = new Dictionary<LotteryType, LotteryFeature>();
            _lotteryEngines = new Dictionary<LotteryType, LotteryEngine>();
            foreach (var item in lotteryConfigDataDic)
            {
                LoadLotteryEngine(item.Key,item.Value);
            }
        }

        private static void LoadLotteryEngine(string lotteryTypeStr,string lotteryConfigData)
        {
            var lotteryType = Utils.StringConvertEnum<LotteryType>(lotteryTypeStr);
            _lotteryEngines[lotteryType] = new LotteryEngine(lotteryType, lotteryConfigData);
        }

        private LotteryEngine(LotteryType lotteryType, string lotteryConfigData)
        {
            LoadLotteryFeature(lotteryType, lotteryConfigData);
        }

        private void LoadLotteryFeature(LotteryType lotteryType, string lotteryConfigData)
        {
            _lotteryFeatures[lotteryType] = lotteryConfigData.ToObject<LotteryFeature>();
        }

      
    }
}
