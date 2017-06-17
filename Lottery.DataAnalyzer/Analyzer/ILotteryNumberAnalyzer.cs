using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common.Dependency;
using LotteryService.Common.Enums;

namespace Lottery.DataAnalyzer.Analyzer
{
    public interface ILotteryNumberAnalyzer : ITransientDependency
    {
        /// <summary>
        /// 彩票数据位
        /// </summary>
        NumberInfo NumberInfo { get; }

        /// <summary>
        /// 该位的历史开奖数据
        /// </summary>
        IDictionary<int, PeriodNumberInfo> NumberHistoryDatas { get; }

        /// <summary>
        /// 三区间数据分析数据
        /// </summary>
        IDictionary<ThreeRegion, ThreeRegionDataInfo> ThreeRegionDatas { get; }

        /// <summary>
        /// 统计连号数据
        /// </summary>
        /// <returns></returns>
        IDictionary<int, NumberRateDataInfo> StatisticsNumberRate();

        /// <summary>
        /// 遗漏值分析
        /// </summary>
        /// <returns></returns>
        IDictionary<int, NumberMissingDataInfo> StatisticsMissingValue();

        /// <summary>
        /// 获取某一区间的数据
        /// </summary>
        /// <param name="threeRegion"></param>
        /// <returns></returns>
        ThreeRegionDataInfo GetThreeRegionData(ThreeRegion threeRegion);

        IDictionary<int, NumberRateDataInfo> GetTemperLotteryNumber(TemperShape temperShape);

        /// <summary>
        ///  小数形态  比率
        /// </summary>
        double SamllSizePercent { get;  }

        /// <summary>
        ///  大数形态  比率
        /// </summary>
        double BigSizePercent { get; }

        /// <summary>
        /// 奇数形态 比率
        /// </summary>
        double OddPercent { get; }

        /// <summary>
        /// 偶数形态 比率
        /// </summary>
        double EvenPercent { get; }

    }
}