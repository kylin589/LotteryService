using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Lottery.Entities;
using LotteryService.Common.Dependency;
using System;
using LotteryService.Common.Enums;

namespace Lottery.DataAnalyzer.Analyzer
{
    public class LotteryNumberAnalyzer : ILotteryNumberAnalyzer
    {
        private NumberInfo _numberInfo;
        
        // 数据包
        private IList<LotteryData> _lotteryDataPackage;

        // 该数据位的历史开奖数据
        private IDictionary<int, PeriodNumberInfo> _numberHistoryDatas;

        private IDictionary<ThreeRegion, ThreeRegionDataInfo> _threeRegionDatas;

        private int smallSizeCount = 0;
        private int oddCount = 0;

        public LotteryNumberAnalyzer(IList<LotteryData> lotteryDataPackage, NumberInfo numberInfo)
        {
            _lotteryDataPackage = lotteryDataPackage;
            _numberInfo = numberInfo;

            InitHistoryDatas();
        }

        private void InitHistoryDatas()
        {
            var numberHistoryDatas = new ConcurrentDictionary<int, PeriodNumberInfo>();
            _threeRegionDatas = new ConcurrentDictionary<ThreeRegion, ThreeRegionDataInfo>();

            _lotteryDataPackage.AsParallel().ForAll(lotteryData =>
            {
                // 
                var lotteryNum =
                    lotteryData.Data.Split(',').Select(p => Convert.ToInt32(p)).ToList()[_numberInfo.KeyNumber - 1];

                var periodNumberInfo = new PeriodNumberInfo(lotteryData.Period, lotteryNum, _numberInfo);
                numberHistoryDatas[lotteryData.Period] = periodNumberInfo;
                if (lotteryNum <= NumberInfo.SizeCriticalValue)
                {
                    smallSizeCount++;
                }
                if (lotteryNum % 2 == 1)
                {
                    oddCount++;
                }

                if (!_threeRegionDatas.Keys.Contains(periodNumberInfo.ThreeRegionShape))
                {
                    _threeRegionDatas[periodNumberInfo.ThreeRegionShape] = new ThreeRegionDataInfo(periodNumberInfo.ThreeRegionShape, _lotteryDataPackage.Count, _numberInfo);
                    _threeRegionDatas[periodNumberInfo.ThreeRegionShape].PutThisRegionData(new KeyValuePair<int, PeriodNumberInfo>(lotteryData.Period, periodNumberInfo));
                }
                else
                {
                    _threeRegionDatas[periodNumberInfo.ThreeRegionShape].PutThisRegionData(new KeyValuePair<int, PeriodNumberInfo>(lotteryData.Period, periodNumberInfo));
                }

            });
            _numberHistoryDatas = numberHistoryDatas.OrderByDescending(p => p.Key).ToDictionary(p => p.Key, p => p.Value);
        }

        /// <summary>
        /// 数据位信息
        /// </summary>
        public NumberInfo NumberInfo => _numberInfo;

        /// <summary>
        /// 该位的历史开奖数据
        /// </summary>
        public IDictionary<int, PeriodNumberInfo> NumberHistoryDatas => _numberHistoryDatas;
        
        /// <summary>
        /// 三区间数据分析数据
        /// </summary>
        public IDictionary<ThreeRegion, ThreeRegionDataInfo> ThreeRegionDatas => _threeRegionDatas;

        /// <summary>
        /// 统计该位置彩票出现的频率
        /// </summary>
        /// <returns></returns>
        public IDictionary<int, NumberRateDataInfo> StatisticsNumberRate()
        {
            var numberRateDic = new SortedDictionary<int, NumberRateDataInfo>();

            foreach (var dataItem in _numberHistoryDatas)
            {
                if (numberRateDic.ContainsKey(dataItem.Value.LotteryNum))
                {
                    numberRateDic[dataItem.Value.LotteryNum].PutHistoryItemData(new KeyValuePair<int, PeriodNumberInfo>(dataItem.Key, NumberHistoryDatas.First(p => p.Key == dataItem.Key).Value));
                }
                else
                {
                    numberRateDic.Add(dataItem.Value.LotteryNum, new NumberRateDataInfo(new KeyValuePair<int, PeriodNumberInfo>(dataItem.Key, NumberHistoryDatas.First(p => p.Key == dataItem.Key).Value), _lotteryDataPackage.Count));
                }
            }
            for (int i = _numberInfo.MinValue; i <= _numberInfo.MaxValue; i++)
            {
                if (!numberRateDic.ContainsKey(i))
                {
                    numberRateDic.Add(i, new NumberRateDataInfo(_lotteryDataPackage.Count));
                }
            }

            return numberRateDic.OrderByDescending(p => p.Value.RepeatNum).ThenByDescending(p => p.Key).ToDictionary(p => p.Key, p => p.Value);
        }


        /// <summary>
        /// 统计该位置彩票出现的遗漏值
        /// </summary>
        /// <returns></returns>
        public IDictionary<int, NumberMissingDataInfo> StatisticsMissingValue()
        {
            var missingValueDic = new Dictionary<int, NumberMissingDataInfo>();
            for (int i = NumberInfo.MinValue; i <= NumberInfo.MaxValue; i++)
            {
                missingValueDic[i] = new NumberMissingDataInfo(i)
                {
                    MaxMissingValue = ComputeMaxMissingValue(i),
                    MissingValue = ComputeMissingValue(i),
                    AvgMissingValue = ComputeAvgMissingValue(i)
                };
            }
            return missingValueDic;
        }

        public ThreeRegionDataInfo GetThreeRegionData(ThreeRegion threeRegion)
        {
            if (ThreeRegionDatas.ContainsKey(threeRegion))
            {
                return ThreeRegionDatas[threeRegion];
            }
            return null;
        }

        public IDictionary<int, NumberRateDataInfo> GetTemperLotteryNumber(TemperShape temperShape)
        {
            IDictionary<int, NumberRateDataInfo> temperLotteryNumbers = null;
            var numberRatesDic = StatisticsNumberRate();

            var statisticsNumberCount = numberRatesDic.Count;

            var hotOrColdNumber = statisticsNumberCount / 3;
            var mildNumber = statisticsNumberCount - hotOrColdNumber * 2;

            switch (temperShape)
            {
                case TemperShape.Hot:
                    temperLotteryNumbers = numberRatesDic.Take(hotOrColdNumber).ToDictionary(p => p.Key, p => p.Value);
                    break;
                case TemperShape.Mild:
                    temperLotteryNumbers = numberRatesDic.Skip(hotOrColdNumber).Take(mildNumber).ToDictionary(p => p.Key, p => p.Value);
                    break;

                case TemperShape.Cold:
                    temperLotteryNumbers = numberRatesDic.Skip(hotOrColdNumber + mildNumber).ToDictionary(p => p.Key, p => p.Value);
                    break;

            }
            return temperLotteryNumbers;
        }

        public double SamllSizePercent
        {
            get { return Math.Round((double)smallSizeCount / _lotteryDataPackage.Count, 2); }
        }

        public double BigSizePercent
        {
            get { return Math.Round((double)(_lotteryDataPackage.Count - smallSizeCount) / _lotteryDataPackage.Count, 2); }
        }

        public double OddPercent
        {
            get { return Math.Round((double)oddCount / _lotteryDataPackage.Count, 2); }
        }

        public double EvenPercent
        {
            get { return Math.Round((double)(_lotteryDataPackage.Count - oddCount) / _lotteryDataPackage.Count, 2); }
        }

        #region 私有方法

        private int ComputeAvgMissingValue(int lotteryNumber)
        {
            var count = 0;
            var avgMissingValue = 0;
            foreach (var historyData in _numberHistoryDatas)
            {
                if (historyData.Value.LotteryNum == lotteryNumber)
                {
                    count++;
                }
            }
            if (count == 0)
            {
                return avgMissingValue;
            }
            avgMissingValue = Convert.ToInt32((_numberHistoryDatas.Count - count) / count);
            return avgMissingValue;
        }

        private int ComputeMissingValue(int lotteryNumber)
        {
            var count = 0;
            var missingValue = 0;
            foreach (var historyData in _numberHistoryDatas)
            {
                if (historyData.Value.LotteryNum == lotteryNumber)
                {
                    missingValue = count;

                    break;
                }
                count++;
            }
            return missingValue;
        }

        private int ComputeMaxMissingValue(int lotteryNumber)
        {
            var count = 0;
            var maxMissingValue = 0;
            foreach (var historyData in _numberHistoryDatas)
            {
                if (historyData.Value.LotteryNum == lotteryNumber)
                {
                    if (maxMissingValue < count)
                    {
                        maxMissingValue = count;
                        count = 0;
                    }
                    continue;
                }
                count++;
            }
            return maxMissingValue;
        }

        #endregion

   
    }
}