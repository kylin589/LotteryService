using System.Collections.Generic;

namespace Lottery.Engine.Perdictor
{
    interface ILotteryTrackNumber
    {
        
        IList<object> TrackNumber();

        /// <summary>
        /// 是否预测正确
        /// </summary>
        /// <returns></returns>
        bool AssertPredictData();
    }
}