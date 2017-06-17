using Lottery.Entities;
using LotteryService.Common.Enums;

namespace Lottery.DataAnalyzer.Analyzer
{
    public class PeriodNumberInfo
    {
        private int _lotteryNum;
        private int _period;
        private NumberInfo _numberInfo;

        private readonly int _firstRegionCriticalValue;
        private readonly int _secondRegionCriticalValue;
        private readonly int _thirdRegionCriticalValue;

        public PeriodNumberInfo(int period, int lotteryNum, NumberInfo numberInfo)
        {
            _period = period;
            _lotteryNum = lotteryNum;
            _numberInfo = numberInfo;

            _firstRegionCriticalValue = _numberInfo.ComputeCriticalValue(ThreeRegion.FirstRegion);
            _secondRegionCriticalValue = _numberInfo.ComputeCriticalValue(ThreeRegion.SecondRegion);
            _thirdRegionCriticalValue = _numberInfo.ComputeCriticalValue(ThreeRegion.ThirdRegion);
        }

        public int Period => _period;

        public int LotteryNum => _lotteryNum;

        public ThreeRegion ThreeRegionShape
        {
            get
            {
                if (_lotteryNum <= _firstRegionCriticalValue)
                {
                    return ThreeRegion.FirstRegion;
                }
                if (_lotteryNum > _firstRegionCriticalValue && _lotteryNum <= _secondRegionCriticalValue)
                {
                    return ThreeRegion.SecondRegion;
                }
                return ThreeRegion.ThirdRegion;
            }
        }

       
    }
}