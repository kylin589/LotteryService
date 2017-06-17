using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lottery.DataAnalyzer;
using Lottery.Engine.Perdictor;
using Lottery.Entities;
using LotteryService.Common.Enums;
using Microsoft.Practices.ServiceLocation;
using LotteryService.Application.Lottery;
using LotteryService.Common;
using LotteryService.Common.Extensions;
using LotteryService.Common.Tools;

namespace Lottery.Engine
{
    /// <summary>
    /// 彩票数据分析引擎(核心对象)
    /// </summary>
    public class LotteryEngine
    {
        private static IDictionary<LotteryType, LotteryFeature> _lotteryFeatures;

        private static IDictionary<LotteryType, LotteryEngine> _lotteryEngines;

        private static ILotteryConfigAppService _lotteryFeatureLoader;

        private LotteryType _lotteryType;

        private IList<LotteryData> _histroyLotteryDatas;

        private LotteryFeature _lotteryFeature;

        private IList<LotteryPlan> _lotteryPlans;

        private ICollection<LotteryAnalyseNorm> _lotteryAnalyseNorms;

        private ILotteryDataManager _lotteryDataManager;

        private ILotteryAnalyseNormManager _lotteryAnalyseNormManager;

        /// <summary>
        /// 加载数据引擎，系统启动时，即刻加载数据引擎
        /// </summary>
        public static void Init()
        {
        }

        /// <summary>
        /// 初始化系统资源
        /// </summary>
        static LotteryEngine()
        {
            _lotteryFeatureLoader = ServiceLocator.Current.GetInstance<ILotteryConfigAppService>();

            var lotteryConfigDataDic = _lotteryFeatureLoader.GetLotteryConfigs();
            _lotteryFeatures = new Dictionary<LotteryType, LotteryFeature>();
            _lotteryEngines = new Dictionary<LotteryType, LotteryEngine>();
            foreach (var item in lotteryConfigDataDic)
            {
                LoadLotteryEngine(item.Key, item.Value);
            }
        }

        /// <summary>
        /// 加载指定彩种的数据引擎
        /// </summary>
        /// <param name="lotteryTypeStr"></param>
        /// <param name="lotteryConfigData"></param>
        private static void LoadLotteryEngine(string lotteryTypeStr, string lotteryConfigData)
        {
            var lotteryType = Utils.StringConvertEnum<LotteryType>(lotteryTypeStr);
            _lotteryEngines[lotteryType] = new LotteryEngine(lotteryType, lotteryConfigData);
        }

        /// <summary>
        /// 获取指定彩种的数据引擎
        /// </summary>
        /// <param name="lotteryType"></param>
        /// <returns></returns>
        public static LotteryEngine GetLotteryEngine(LotteryType lotteryType)
        {
            return _lotteryEngines[lotteryType];
        }

        public static LotteryFeature GetLotteryFeature(LotteryType lotteryType)
        {
            return _lotteryFeatures[lotteryType];
        }

        /// <summary>
        /// 彩票引擎构造器，必须私有化，不允许外界访问
        /// </summary>
        /// <param name="lotteryType">彩种</param>
        /// <param name="lotteryConfigData">彩票配置信息</param>
        private LotteryEngine(LotteryType lotteryType, string lotteryConfigData)
        {
            _lotteryType = lotteryType;
            _lotteryFeature = lotteryConfigData.ToObject<LotteryFeature>();
            _lotteryFeatures[lotteryType] = _lotteryFeature;
            _lotteryAnalyseNormManager = ServiceLocator.Current.GetInstance<ILotteryAnalyseNormManager>();
            _lotteryAnalyseNorms = _lotteryAnalyseNormManager.LoadLotteryAnalyseNorms(lotteryType);
            _lotteryDataManager = ServiceLocator.Current.GetInstance<ILotteryDataManager>();

            InitLotteryPlan();

            RedisHelper.Set(AppUtils.GetLotteryRedisKey(lotteryType.ToString(), LsConstant.LotteryFeatureRedisKey), lotteryConfigData);

        }

        private void InitLotteryPlan()
        {
            _lotteryPlans = new List<LotteryPlan>();
            foreach (var normGroup in _lotteryFeature.LotteryNorm.NormGroup)
            {
                foreach (var plan in normGroup.Plans)
                {
                    _lotteryPlans.Add(plan);
                }
            }
        }

        public ICollection<LotteryAnalyseNorm> LotteryAnalyseNorms
        {
            get
            {
                _lotteryAnalyseNorms = _lotteryAnalyseNormManager.LoadLotteryAnalyseNorms(_lotteryType);
                return _lotteryAnalyseNorms;
            }
        }

        public IList<LotteryData> HistoryLotteryDataAll
        {
            get { return _lotteryDataManager.GetHistoryLotteryDatas(_lotteryType); }
        }

        public IList<LotteryData> GetLotteryDatas(int count)
        {
            return _lotteryDataManager.GetHistoryLotteryDatas(_lotteryType, count);
        }

        public void CalculateNextLotteryData()
        {
            var option = new ParallelOptions()
            {
                MaxDegreeOfParallelism = LsConstant.MaxDegreeOfParallelism,
            };

            Parallel.ForEach(LotteryAnalyseNorms, option, norm =>
            {
                var lotteryPredictor = new LotteryPerdictor(norm, this);
                lotteryPredictor.ComputeTrackNumber();
            });
        }

        public LotteryPlan GetLotteryPlan(int planId)
        {
            return _lotteryPlans.First(p => p.PlanId == planId);
        }
    }
}
