using System.Collections.Generic;
using LotteryService.Common.Enums;
using LotteryService.Domain.Interfaces.Repository.Dapper;
using LotteryService.Domain.Interfaces.Service;

namespace LotteryService.Domain.Lottery
{
    public class AnylseNormService : IAnylseNormService
    {
        private readonly IAnylseNormDapperReportory _anylseNormDapperReportory;
        private readonly ILotteryAnalyseNormDapperRepostory _lotteryAnalyseNormDapperRepostory;

        public AnylseNormService(IAnylseNormDapperReportory anylseNormDapperReportory,
            ILotteryAnalyseNormDapperRepostory lotteryAnalyseNormDapperRepostory)
        {
            _anylseNormDapperReportory = anylseNormDapperReportory;
            _lotteryAnalyseNormDapperRepostory = lotteryAnalyseNormDapperRepostory;
        }

        public IList<int> GetUserSelectedPlans(string userId, LotteryType lotteryType,out bool isSysDefault)
        {
            return _anylseNormDapperReportory.GetUserSelectedPlans(userId, lotteryType,out isSysDefault);
        }

        public bool UpdateUserLotteryPlan(string userId, LotteryType lotteryType, IList<int> planIds)
        {
            bool isSysDefault;
            var userOldLotteryPlanIds = _anylseNormDapperReportory.GetUserSelectedPlans(userId, lotteryType,
                out isSysDefault);

            var userBasicNorm = _lotteryAnalyseNormDapperRepostory.GetUserBasicNorm(userId, lotteryType);

            if (isSysDefault)
            {
                _anylseNormDapperReportory.InsertUserPlans(userId,lotteryType, userBasicNorm,planIds);
            }
            else
            {
                _anylseNormDapperReportory.UpdateUserPlans(userId, lotteryType, userBasicNorm, planIds, userOldLotteryPlanIds);
            }
            return true;
        }
    }
}