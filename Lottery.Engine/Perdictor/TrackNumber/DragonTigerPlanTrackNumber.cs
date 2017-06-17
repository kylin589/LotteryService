using System.Collections.Generic;
using Lottery.Entities;

namespace Lottery.Engine.Perdictor
{
    class DragonTigerPlanTrackNumber : ILotteryTrackNumber
    {

        public DragonTigerPlanTrackNumber(LotteryPerdictor perdictor,LotteryPlan lotteryPlan)
        {

        }

        public IList<object> TrackNumber()
        {
            throw new System.NotImplementedException();
        }
    }
}