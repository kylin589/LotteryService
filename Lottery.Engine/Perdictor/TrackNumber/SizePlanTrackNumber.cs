using System.Collections.Generic;
using Lottery.Entities;

namespace Lottery.Engine.Perdictor
{
     class SizePlanTrackNumber : ILotteryTrackNumber
    {
        private LotteryPerdictor lotteryPerdictor;
        private LotteryPlan lotteryPlan;

        public SizePlanTrackNumber(LotteryPerdictor lotteryPerdictor, LotteryPlan lotteryPlan)
        {
            this.lotteryPerdictor = lotteryPerdictor;
            this.lotteryPlan = lotteryPlan;
        }

        public IList<object> TrackNumber()
        {
            throw new System.NotImplementedException();
        }
    }
}