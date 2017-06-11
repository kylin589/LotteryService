using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using LotteryService.Common.Enums;
using LotteryService.Common.Extensions;
using LotteryService.Data.Repository.Dapper.Common;
using LotteryService.Domain.Interfaces.Repository.Dapper;

namespace LotteryService.Data.Repository.Dapper.Lottery
{
    public class AnylseNormDapperReportory : DapperRepository,IAnylseNormDapperReportory
    {
        public IList<int> GetUserSelectedPlans(string userId, LotteryType lotteryType)
        {
            var planIds = new List<int>();
            using (var cn = LotteryDbConnection)
            {
                var sqlStr1 = "SELECT [LotteryAnalyseNormId] FROM [dbo].[UserAnylseNorms] " +
                              "  WHERE UserId = @UserId AND LotteryType =@LotteryType";
                var lotteryAnalyseNormIds = cn.Query<string>(sqlStr1, new
                {
                    UserId = userId,
                    LotteryType = lotteryType
                });
                if (lotteryAnalyseNormIds !=null && lotteryAnalyseNormIds.Any())
                {
                    var sqlStr2 = "SELECT PlanId FROM dbo.LotteryAnalyseNorms WHERE Id IN(@LotteryAnalyseNormIds)";
                    planIds = cn.Query<int>(sqlStr2, new
                    {
                        LotteryAnalyseNormIds = lotteryAnalyseNormIds.ToSplitString(",")
                    }).ToList();
                }
                else
                {
                    var sqlStr3 =
                        "SELECT PlanId FROM [dbo].[LotteryAnalyseNorms] WHERE IsDefault = @IsDefault AND LotteryType = @LotteryType";
                    planIds = cn.Query<int>(sqlStr3, new
                    {
                        IsDefault = 1,
                        LotteryType = lotteryType.ToString()

                    }).ToList();
                }
                return planIds;
            }
        }
    }
}