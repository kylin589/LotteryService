using System;
using System.Collections.Generic;
using System.Linq;
using Lottery.Entities;
using LotteryService.Common.Enums;

namespace Lottery.Engine.Perdictor
{
    class NumPlanTrackNumber : BasePlanTrackNumber
    {

        public NumPlanTrackNumber(LotteryPerdictor lotteryPerdictor) : base(lotteryPerdictor)
        {
        }

        public override IList<object> TrackNumber()
        {
             return new List<object>()
             {
                 1,
                 3,
                 6,
             };
        }

        protected override bool AssertPredictDataResult()
        {
            return AssertPredictNumberDataResult();
        }

    }
}