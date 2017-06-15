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
using LotteryService.Common;
using LotteryService.Common.Extensions;
using LotteryService.Common.Tools;

namespace Lottery.Engine
{
    public class LotteryEngine
    {
        private static IDictionary<LotteryType, LotteryFeature> _lotteryFeatures;

        private static IDictionary<LotteryType, LotteryEngine> _lotteryEngines;
      
        private LotteryType _lotteryType;

        private IList<LotteryData> _histroyLotteryDatas;

        private LotteryFeature _lotteryFeature;

        private ICollection<LotteryAnalyseNorm> _lotteryAnalyseNorms;

        private ILotteryDataManager _lotteryDataManager;
      
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

        public static LotteryEngine GetLotteryEngine(LotteryType lotteryType)
        {
            return _lotteryEngines[lotteryType];
        }

        public static LotteryFeature GetLotteryFeature(LotteryType lotteryType)
        {
            return _lotteryFeatures[lotteryType];
        }

        private LotteryEngine(LotteryType lotteryType, string lotteryConfigData)
        {
            _lotteryType = lotteryType;
            _lotteryFeature = lotteryConfigData.ToObject<LotteryFeature>();
            _lotteryFeatures[lotteryType] = _lotteryFeature;

            var lotteryAnalyseNormManager = ServiceLocator.Current.GetInstance<ILotteryAnalyseNormManager>();           
            _lotteryAnalyseNorms = lotteryAnalyseNormManager.LoadLotteryAnalyseNorms(lotteryType);            
            _lotteryDataManager = ServiceLocator.Current.GetInstance<ILotteryDataManager>();

            RedisHelper.Set(AppUtils.GetLotteryRedisKey(lotteryType.ToString(), LsConstant.LotteryFeatureRedisKey), lotteryConfigData);

        }

        public IList<LotteryData> HistoryLotteryDatas
        {
            get { return _lotteryDataManager.GetHistoryLotteryDatas(_lotteryType); }
        }

        public void CalculateNextLotteryData()
        {

        }
    }
}
