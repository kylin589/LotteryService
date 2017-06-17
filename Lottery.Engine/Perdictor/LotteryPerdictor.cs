using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.DataAnalyzer.Analyzer;
using Lottery.Entities;
using LotteryService.Common.Enums;
using LotteryService.Common.Excetions;

namespace Lottery.Engine.Perdictor
{
    class LotteryPerdictor
    {
        private LotteryEngine _lotteryEngine;
        private LotteryAnalyseNorm _norm;

        private ILotteryDataAnlyer _basicLotteryDataAnlyer;
        private ILotteryDataAnlyer _unitLotteryDataAnlyer;

        public LotteryPerdictor(LotteryAnalyseNorm norm, LotteryEngine lotteryEngine)
        {
            _norm = norm;
            _lotteryEngine = lotteryEngine;
            _basicLotteryDataAnlyer = new LotteryDataAnlyer(lotteryEngine.LotteryType,lotteryEngine.NumberInfos, _norm.BasicHistoryCount);
            _unitLotteryDataAnlyer = new LotteryDataAnlyer(lotteryEngine.LotteryType, lotteryEngine.NumberInfos, _norm.UnitHistoryCount);

        }

        public LotteryEngine LotteryEngine => _lotteryEngine;

        public LotteryAnalyseNorm LotteryNorm => _norm;

        public void ComputeTrackNumber()
        {
            var lotteryPlan = _lotteryEngine.GetLotteryPlan(_norm.PlanId);
            ILotteryTrackNumber lotteryTrackNumber = null;
            switch (lotteryPlan.PlanType)
            {
                case PlanType.DragonTigerPlan:
                    lotteryTrackNumber = new DragonTigerPlanTrackNumber(this, lotteryPlan);
                    break;
                case PlanType.Kill:
                    lotteryTrackNumber = new KillPlanTrackNumber(this, lotteryPlan);
                    break;
                case PlanType.NumPlan:
                    lotteryTrackNumber = new NumPlanTrackNumber(this, lotteryPlan);
                    break;
                case PlanType.RankPlan:
                    lotteryTrackNumber = new RankPlanTrackNumber(this, lotteryPlan);
                    break;
                case PlanType.SizePlan:
                    lotteryTrackNumber = new SizePlanTrackNumber(this, lotteryPlan);
                    break;
                default:
                    throw new LSException("不存在该类型的彩票计划类型");

            }

            var lotteryData = LotteryEngine.GetLotteryDatas(_norm.BasicHistoryCount);

        }
    }
}
