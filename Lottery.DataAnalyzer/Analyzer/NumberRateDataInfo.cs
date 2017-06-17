using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lottery.DataAnalyzer.Analyzer
{
    public class NumberRateDataInfo
    {
        private int _dataPackageCount;
        private IDictionary<int, PeriodNumberInfo> _historyNumberData;

        public int RepeatNum { private set; get; }

        public NumberRateDataInfo(int dataPackageCount)
        {
            _dataPackageCount = dataPackageCount;
            RepeatNum = 0;
        }

        public NumberRateDataInfo(KeyValuePair<int, PeriodNumberInfo> historyItemData, int dataPackageCount)
        {
            _historyNumberData = new SortedDictionary<int, PeriodNumberInfo>();
            _historyNumberData.Add(historyItemData);
            _dataPackageCount = dataPackageCount;
            RepeatNum = 1;
        }

        public void PutHistoryItemData(KeyValuePair<int, PeriodNumberInfo> historyItemData)
        {
            RepeatNum += 1;
            _historyNumberData.Add(historyItemData);
        }

        public double RepeatPercent
        {
            get
            {
                Debug.Assert(_dataPackageCount != 0, "_dataPackageCount != 0");
                var repeatNum = RepeatNum;
                return Math.Round((double)repeatNum / _dataPackageCount * 100, 2);
            }
        }
    }
}