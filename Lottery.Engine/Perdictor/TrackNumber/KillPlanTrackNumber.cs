using System.Collections.Generic;
using Lottery.Entities;

namespace Lottery.Engine.Perdictor
{
    class KillPlanTrackNumber : ILotteryTrackNumber
    {
        private LotteryPerdictor lotteryPerdictor;
        private LotteryPlan lotteryPlan;

        public KillPlanTrackNumber(LotteryPerdictor lotteryPerdictor, LotteryPlan lotteryPlan)
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