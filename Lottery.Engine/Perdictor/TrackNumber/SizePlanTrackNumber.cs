using System.Collections.Generic;
using Lottery.Entities;
using System.Linq;

namespace Lottery.Engine.Perdictor
{
    class SizePlanTrackNumber : BasePlanTrackNumber
    {
        private const string BigValue = "大";
        private const string SmallValue = "小";

        public SizePlanTrackNumber(LotteryPerdictor lotteryPerdictor) : base(lotteryPerdictor)
        {          
        }

        public override IList<object> TrackNumber()
        {
            return new List<object>()
            {
                "大"
            };
        }

        protected override bool AssertPredictDataResult()
        {
            var key = _lotteryPerdictor.LotteryPlan.KeyNumber[0];
            var keyNumberInfo = _lotteryPerdictor.LotteryEngine.GetNumberInfo(key - 1);
            var keyNumber = _lotteryPerdictor.LotteryEngine.GetLastLotteryDataNumber(key);

            var currentPredictDataValue = keyNumber > keyNumberInfo.SizeCriticalValue ? BigValue : SmallValue;
            if (_lotteryPerdictor.LotteryPredictData.PredictedNum.Split(',').Contains(currentPredictDataValue))
            {
                return true;
            }
            return false;
        }
    }
}