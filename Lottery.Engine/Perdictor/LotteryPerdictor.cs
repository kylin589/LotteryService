using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.DataAnalyzer.Analyzer;
using Lottery.Entities;
using LotteryService.Application.Lottery;
using LotteryService.Common.Enums;
using LotteryService.Common.Excetions;
using LotteryService.Common.Extensions;
using Microsoft.Practices.ServiceLocation;

namespace Lottery.Engine.Perdictor
{
    class LotteryPerdictor
    {
        private readonly LotteryEngine _lotteryEngine;
        private readonly LotteryAnalyseNorm _norm;

        private readonly ILotteryPredictDataAppService _lotteryPredictDataAppService;
        private readonly LotteryPlan _lotteryPlan;

        private ILotteryTrackNumber _lotteryTrackNumber = null;
        private  LotteryPredictData _lotteryPredictData;
        private bool _isNeedRecalculate = false;


        public LotteryPerdictor(LotteryAnalyseNorm norm, LotteryEngine lotteryEngine)
        {
            _norm = norm;
            _lotteryEngine = lotteryEngine;
            _lotteryPlan = _lotteryEngine.GetLotteryPlan(_norm.PlanId);

            _lotteryPredictDataAppService = ServiceLocator.Current.GetInstance<ILotteryPredictDataAppService>();

            Init();

        }

        private void Init()
        {
            switch (_lotteryPlan.PlanType)
            {
                case PlanType.DragonTigerPlan:
                    _lotteryTrackNumber = new DragonTigerPlanTrackNumber(this);
                    break;
                case PlanType.Kill:
                    _lotteryTrackNumber = new KillPlanTrackNumber(this);
                    break;
                case PlanType.NumPlan:
                    _lotteryTrackNumber = new NumPlanTrackNumber(this);
                    break;
                case PlanType.RankPlan:
                    _lotteryTrackNumber = new RankPlanTrackNumber(this);
                    break;
                case PlanType.SizePlan:
                    _lotteryTrackNumber = new SizePlanTrackNumber(this);
                    break;
                default:
                    throw new LSException("不存在该类型的彩票计划类型");

            }

            _lotteryPredictData = _lotteryPredictDataAppService.GetCurrentLotteryPredictData(_norm.Id,LotteryEngine.LastLotteryData.Period);
            if (_lotteryPredictData == null)
            {
                _lotteryPredictData = new LotteryPredictData()
                {
                    NormId = LotteryNorm.Id,
                    CurrentPredictPeriod = LotteryEngine.ForecastPeriod,
                    StartPeriod = LotteryEngine.ForecastPeriod,
                    EndPeriod = LotteryEngine.ForecastPeriod + LotteryNorm.PlanCycle - 1,
                    MinorCycle = 1,
                    PredictedResult = PredictionResult.NoLottery,
                };
                _isNeedRecalculate = true;
            }
            else if (_lotteryTrackNumber.AssertPredictData() && !_isNeedRecalculate)
            {
                _lotteryPredictData.PredictedResult = PredictionResult.Right;
                _lotteryPredictData.EndPeriod = LotteryEngine.LastLotteryData.Period;
              //  _lotteryPredictData.MinorCycle += 1;
                _lotteryPredictData.ModifyTime = DateTime.Now;
                _lotteryPredictDataAppService.Update(_lotteryPredictData);

                _lotteryPredictData = new LotteryPredictData()
                {
                    NormId = LotteryNorm.Id,
                    CurrentPredictPeriod = LotteryEngine.ForecastPeriod,
                    StartPeriod = LotteryEngine.ForecastPeriod,
                    EndPeriod = LotteryEngine.ForecastPeriod + LotteryNorm.PlanCycle - 1,
                    MinorCycle = 1,
                    PredictedResult = PredictionResult.NoLottery,
                };

                _isNeedRecalculate = true;
            }
            else
            {
                if (_lotteryPredictData.EndPeriod >= LotteryEngine.ForecastPeriod)
                {
                    _lotteryPredictData.CurrentPredictPeriod = LotteryEngine.ForecastPeriod;
                    _lotteryPredictData.MinorCycle += 1;
                    _lotteryPredictData.ModifyTime = DateTime.Now;
                    _lotteryPredictDataAppService.Update(_lotteryPredictData);

                    _isNeedRecalculate = false;
                }
                else
                {
                    _lotteryPredictData.PredictedResult = PredictionResult.Error;
                    _lotteryPredictData.ModifyTime = DateTime.Now;
                    _lotteryPredictDataAppService.Update(_lotteryPredictData);

                    _lotteryPredictData = new LotteryPredictData()
                    {
                        NormId = LotteryNorm.Id,
                        CurrentPredictPeriod = LotteryEngine.ForecastPeriod,
                        StartPeriod = LotteryEngine.ForecastPeriod,
                        EndPeriod = LotteryEngine.ForecastPeriod + LotteryNorm.PlanCycle - 1,
                        MinorCycle = 1,
                        PredictedResult = PredictionResult.NoLottery,
                    };

                    _isNeedRecalculate = true;
                }
            }
        }

        public LotteryEngine LotteryEngine => _lotteryEngine;

        public LotteryAnalyseNorm LotteryNorm => _norm;

        public LotteryPredictData LotteryPredictData => _lotteryPredictData;

        public LotteryPlan LotteryPlan => _lotteryPlan;

        public bool IsNeedRecalculate => _isNeedRecalculate;

        public void ComputeTrackNumber()
        {
            if (_isNeedRecalculate)
            {
                var perdictorData = _lotteryTrackNumber.TrackNumber().ToSplitString(",");
                _lotteryPredictData.PredictedNum = perdictorData;
                _lotteryPredictDataAppService.Insert(_lotteryPredictData);
            }
        }
    }
}
