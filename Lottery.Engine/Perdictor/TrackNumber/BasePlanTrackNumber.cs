using System;
using System.Collections.Generic;
using System.Linq;
using Lottery.DataAnalyzer.Analyzer;
using LotteryService.Common.Enums;

namespace Lottery.Engine.Perdictor
{
    abstract class BasePlanTrackNumber : ILotteryTrackNumber
    {
        protected readonly ILotteryDataAnlyer _basicLotteryDataAnlyer;
        protected readonly ILotteryDataAnlyer _unitLotteryDataAnlyer;

        protected readonly LotteryPerdictor _lotteryPerdictor;

        protected BasePlanTrackNumber(LotteryPerdictor perdictor)
        {
            _lotteryPerdictor = perdictor;
            _basicLotteryDataAnlyer = new LotteryDataAnlyer(_lotteryPerdictor.LotteryEngine.LotteryType, _lotteryPerdictor.LotteryEngine.NumberInfos, _lotteryPerdictor.LotteryNorm.BasicHistoryCount);
            _unitLotteryDataAnlyer = new LotteryDataAnlyer(_lotteryPerdictor.LotteryEngine.LotteryType, _lotteryPerdictor.LotteryEngine.NumberInfos, _lotteryPerdictor.LotteryNorm.UnitHistoryCount);
        }

        public abstract IList<object> TrackNumber();

        public virtual bool AssertPredictData()
        {
            if (!_lotteryPerdictor.IsNeedRecalculate)
            {
                return AssertPredictDataResult();
            }
            return false;
        }

        protected abstract bool AssertPredictDataResult();

        protected virtual bool AssertPredictNumberDataResult()
        {
            var predictData =
                _lotteryPerdictor.LotteryPredictData.PredictedNum.Split(',')
                .Select(p => Convert.ToInt32(p))
                .ToArray();
            if (_lotteryPerdictor.LotteryPlan.ForecastType == ForecastType.Single)
            {
                var key = _lotteryPerdictor.LotteryPlan.KeyNumber[0];
                var keyNumberInfo = _lotteryPerdictor.LotteryEngine.GetNumberInfo(key);

                var keyNumber = _lotteryPerdictor.LotteryEngine.GetLastLotteryDataNumber(keyNumberInfo.KeyNumber);

                if (predictData.Contains(keyNumber))
                {
                    return true;
                }
                return false;

            }
            else if (_lotteryPerdictor.LotteryPlan.ForecastType == ForecastType.Multiple)
            {

                var keys = _lotteryPerdictor.LotteryPlan.KeyNumber;
                foreach (var key in keys)
                {
                    var keyNumberInfo = _lotteryPerdictor.LotteryEngine.GetNumberInfo(key);
                    var keyNumber = _lotteryPerdictor.LotteryEngine.GetLastLotteryDataNumber(keyNumberInfo.KeyNumber);
                    if (predictData.Contains(keyNumber))
                    {
                        return true;
                    }
                }
                return false;

            }
            return false;
        }
    }
}