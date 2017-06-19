using System.Collections.Generic;
using Lottery.Entities;
using System.Linq;

namespace Lottery.Engine.Perdictor
{
    class RankPlanTrackNumber : BasePlanTrackNumber
    {
        private LotteryPerdictor lotteryPerdictor;

        private readonly List<string> ArrayRanks;

        private readonly int _key;
        private readonly NumberInfo _keyNumberInfo;

        private NumberInfo _numberInfo;

        public RankPlanTrackNumber(LotteryPerdictor lotteryPerdictor) : base(lotteryPerdictor)
        {
            _key = _lotteryPerdictor.LotteryPlan.KeyNumber[0];
            _numberInfo = _lotteryPerdictor.LotteryEngine.GetNumberInfo(_key - 1);
            ArrayRanks = new List<string>();
            for (int i = _numberInfo.MaxValue; i <= _numberInfo.MaxValue; i++)
            {
                ArrayRanks.Add($"第{i}名");
            }


        }

        public override IList<object> TrackNumber()
        {
            return new List<object>()
            {
                "第1名",
                "第2名",
            };
        }

        protected override bool AssertPredictDataResult()
        {
            var keyRank = _lotteryPerdictor.LotteryEngine.GetLastLotteryDataNumberRank(_key);
            var keyRankVal = $"第{keyRank}名";

            if (_lotteryPerdictor.LotteryPredictData.PredictedNum.Split(',').Contains(keyRankVal))
            {
                return true;
            }
            return false;
        }
    }
}