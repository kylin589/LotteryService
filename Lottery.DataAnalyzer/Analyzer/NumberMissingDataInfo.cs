using System;

namespace Lottery.DataAnalyzer.Analyzer
{
    public class NumberMissingDataInfo
    {
        private readonly int _lotteryNum;

        public NumberMissingDataInfo(int lotteryNum)
        {
            _lotteryNum = lotteryNum;
        }

        public int LotteryNum
        {
            get { return _lotteryNum; }
        }

        /// <summary>
        /// 当前遗漏值
        /// </summary>
        public int MissingValue { get; set; }

        /// <summary>
        /// 最大遗漏值
        /// </summary>
        public int MaxMissingValue { get; set; }

        /// <summary>
        /// 平均遗漏值
        /// </summary>
        public int AvgMissingValue { get; set; }

        public double PreValue
        {
            get { return Math.Round((double)AvgMissingValue / MaxMissingValue, 2); }
        }
    }
}