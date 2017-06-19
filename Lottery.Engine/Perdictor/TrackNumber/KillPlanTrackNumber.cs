using System.Collections.Generic;
using Lottery.Entities;
using LotteryService.Common.Enums;

namespace Lottery.Engine.Perdictor
{
    class KillPlanTrackNumber : BasePlanTrackNumber
    {

        public KillPlanTrackNumber(LotteryPerdictor lotteryPerdictor) : base(lotteryPerdictor)
        {
        }

        public override IList<object> TrackNumber()
        {
            return new List<object>()
            {
                2,3,7,8
            };
        }

        protected override bool AssertPredictDataResult()
        {
            return AssertPredictNumberDataResult();
        }
    }
}