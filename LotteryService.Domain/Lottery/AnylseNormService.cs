using System.Collections.Generic;
using LotteryService.Common.Enums;
using LotteryService.Domain.Interfaces.Repository.Dapper;
using LotteryService.Domain.Interfaces.Service;

namespace LotteryService.Domain.Lottery
{
    public class AnylseNormService : IAnylseNormService
    {
        private readonly IAnylseNormDapperReportory _anylseNormDapperReportory;

        public AnylseNormService(IAnylseNormDapperReportory anylseNormDapperReportory)
        {
            _anylseNormDapperReportory = anylseNormDapperReportory;
        }

        public IList<int> GetUserSelectedPlans(string userId, LotteryType lotteryType)
        {
            return _anylseNormDapperReportory.GetUserSelectedPlans(userId, lotteryType);
        }
    }
}