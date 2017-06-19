using System;
using System.Collections.Generic;
using System.Linq;
using Lottery.DataAnalyzer.Analyzer;
using Lottery.Entities;

namespace Lottery.Engine.Perdictor
{
    class DragonTigerPlanTrackNumber : BasePlanTrackNumber
    {

        private const string DragonValue = "龙";
        private const string TigerValue = "虎";

        public DragonTigerPlanTrackNumber(LotteryPerdictor perdictor) :base(perdictor)
        {
        }

        public override IList<object> TrackNumber()
        {
            return new List<object>()
            {
                "龙"
            };
        }

        protected override bool AssertPredictDataResult()
        {
            var dragonKey = _lotteryPerdictor.LotteryPlan.KeyNumber[0];
            var tigerKey = _lotteryPerdictor.LotteryEngine.GetNumberInfo(dragonKey - 1).MaxValue - dragonKey + 1;
            var drangonNumber = _lotteryPerdictor.LotteryEngine.GetLastLotteryDataNumber(dragonKey);
            var tigerNumber = _lotteryPerdictor.LotteryEngine.GetLastLotteryDataNumber(tigerKey);

            var currentPredictDataValue = drangonNumber > tigerNumber ? DragonValue : TigerValue;
            if (_lotteryPerdictor.LotteryPredictData.PredictedNum.Split(',').Contains(currentPredictDataValue))
            {
                return true;
            }
            return false;
        }
    }
}