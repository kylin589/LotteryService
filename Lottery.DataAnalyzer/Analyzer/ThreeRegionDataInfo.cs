using System.Collections.Concurrent;
using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common.Enums;
using LotteryService.Domain.Logs;

namespace Lottery.DataAnalyzer.Analyzer
{
    public class ThreeRegionDataInfo
    {
        private int _count;
        private ThreeRegion _regionShape;
        private NumberInfo _numberInfo;

        private IDictionary<int, PeriodNumberInfo> _keyNumbers;

        public ThreeRegionDataInfo(ThreeRegion regionShape, int count, NumberInfo numberInfo)
        {
            _regionShape = regionShape;
            _count = count;
            _numberInfo = numberInfo;

            _keyNumbers = new ConcurrentDictionary<int, PeriodNumberInfo>();
        }

        internal void PutThisRegionData(KeyValuePair<int, PeriodNumberInfo> keyValuePair)
        {
            if (keyValuePair.Value.ThreeRegionShape == _regionShape)
            {
                if (!_keyNumbers.ContainsKey(keyValuePair.Key))
                {
                    _keyNumbers.Add(keyValuePair);
                }
                else
                {
                    LogDbHelper.LogDebug("已经存在了当前期" + keyValuePair.Key + "的数据分析",GetType() + "PutThisRegionData");
                }

            };
        }
    }
}