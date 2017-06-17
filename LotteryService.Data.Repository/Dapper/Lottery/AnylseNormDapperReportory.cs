using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Lottery.Entities;
using LotteryService.Common;
using LotteryService.Common.Enums;
using LotteryService.Common.Extensions;
using LotteryService.Data.Repository.Dapper.Common;
using LotteryService.Domain.Interfaces.Repository.Dapper;
using LotteryService.Domain.Logs;

namespace LotteryService.Data.Repository.Dapper.Lottery
{
    public class AnylseNormDapperReportory : DapperRepository, IAnylseNormDapperReportory
    {
        public IList<int> GetUserSelectedPlans(string userId, LotteryType lotteryType, out bool isSysDefault)
        {
            var planIds = new List<int>();
            using (var cn = LotteryDbConnection)
            {
                var sqlStr1 = "SELECT [LotteryAnalyseNormId] FROM [dbo].[UserAnylseNorms] " +
                              "  WHERE UserId = @UserId AND LotteryType =@LotteryType";
                var lotteryAnalyseNormIds = cn.Query<string>(sqlStr1, new
                {
                    UserId = userId,
                    LotteryType = lotteryType.ToString()
                });
                if (lotteryAnalyseNormIds != null && lotteryAnalyseNormIds.Any())
                {
                    var sqlStr2 = "SELECT PlanId FROM dbo.LotteryAnalyseNorms WHERE Id IN @LotteryAnalyseNormIds";

                    planIds = cn.Query<int>(sqlStr2, new
                    {
                        LotteryAnalyseNormIds = lotteryAnalyseNormIds.ToArray()
                    }).ToList();
                    isSysDefault = false;
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
                    isSysDefault = true;
                }
                return planIds;
            }
        }


        public void InsertUserPlans(string userId, LotteryType lotteryType, UserBasicNorm userBasicNorm, IList<int> planIds)
        {
            string sqlStr1 = "INSERT INTO [dbo].[LotteryAnalyseNorms]([Id],[PlanId],[PlanCycle],[LastStartPeriod],[ForecastCount],[BasicHistoryCount] ,[UnitHistoryCount],[HotWeight],[SizeWeight]" +
            " ,[ThreeRegionWeight],[MissingValueWeight],[OddEvenWeight],[Modulus],[LotteryType],[Enable],[IsDefault],[CreatTime],[CreateUserId])" +
            " VALUES(@Id, @PlanId, @PlanCycle, @LastStartPeriod, @ForecastCount, @BasicHistoryCount, @UnitHistoryCount, @HotWeight, @SizeWeight, @ThreeRegionWeight, @MissingValueWeight" +
            ", @OddEvenWeight, @Modulus, @LotteryType, @ENABLE, @IsDefault, GETDATE(), @CreateUserId)";
            string sqlStr2 = "INSERT INTO [dbo].[UserAnylseNorms]([Id],[UserId],[PlanId],[LotteryAnalyseNormId],[LotteryType],[CreatTime])" +
            " VALUES(@Id, @UserId,@PlanId, @LotteryAnalyseNormId, @LotteryType, GETDATE())";

            using (var cn = LotteryDbConnection)
            {
                cn.Open();
                using (var trans = cn.BeginTransaction())
                {
                    try
                    {
                        foreach (var planId in planIds)
                        {
                            var lotteryAnalyseNorm = new LotteryAnalyseNorm()
                            {
                                PlanId = planId,
                                BasicHistoryCount = userBasicNorm.BasicHistoryCount,
                                CreateUserId = userId,
                                ForecastCount = userBasicNorm.ForecastCount,
                                HotWeight = userBasicNorm.HotWeight,
                                LastStartPeriod = 0, // Todo: set LastStartPeriod
                                LotteryType = lotteryType.ToString(),
                                UnitHistoryCount = userBasicNorm.UnitHistoryCount,
                                Modulus = userBasicNorm.Modulus,
                                OddEvenWeight = userBasicNorm.OddEvenWeight,
                                MissingValueWeight = userBasicNorm.MissingValueWeight,
                                ThreeRegionWeight = userBasicNorm.ThreeRegionWeight,
                                SizeWeight = userBasicNorm.SizeWeight,
                                PlanCycle = userBasicNorm.PlanCycle,

                            };
                            var userAnalyseNorm = new UserAnylseNorm()
                            {
                                LotteryAnalyseNormId = lotteryAnalyseNorm.Id,
                                LotteryType = lotteryType.ToString(),
                                UserId = userId,
                                PlanId = planId,

                            };

                            cn.Execute(sqlStr1, lotteryAnalyseNorm, trans);
                            cn.Execute(sqlStr2, userAnalyseNorm, trans);
                        }
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        LogDbHelper.LogError(ex, GetType() + "=>InsertUserPlans");
                        throw ex;
                    }
                }
                

            }
        }

        public void UpdateUserPlans(string userId, LotteryType lotteryType, UserBasicNorm userBasicNorm, IList<int> planIds,
            IList<int> userOldLotteryPlanIds)
        {
            string sqlStr1 = "SELECT * FROM [dbo].[UserAnylseNorms] WHERE UserId=@UserId AND LotteryType=@LotteryType AND PlanId=@PlanId";
            string sqlStr2 = "INSERT INTO [dbo].[UserAnylseNorms]([Id],[UserId],[PlanId],[LotteryAnalyseNormId],[LotteryType],[CreatTime])" +
          " VALUES(@Id, @UserId,@PlanId, @LotteryAnalyseNormId, @LotteryType, GETDATE())";

            string sqlStr2_1 = "INSERT INTO [dbo].[LotteryAnalyseNorms]([Id],[PlanId],[PlanCycle],[LastStartPeriod],[ForecastCount],[BasicHistoryCount] ,[UnitHistoryCount],[HotWeight],[SizeWeight]" +
           " ,[ThreeRegionWeight],[MissingValueWeight],[OddEvenWeight],[Modulus],[LotteryType],[Enable],[IsDefault],[CreatTime],[CreateUserId])" +
           " VALUES(@Id, @PlanId, @PlanCycle, @LastStartPeriod, @ForecastCount, @BasicHistoryCount, @UnitHistoryCount, @HotWeight, @SizeWeight, @ThreeRegionWeight, @MissingValueWeight" +
           ", @OddEvenWeight, @Modulus, @LotteryType, @ENABLE, @IsDefault, GETDATE(), @CreateUserId)";

            string sqlStr3 = "SELECT * FROM [dbo].[LotteryAnalyseNorms] WHERE Id=@Id";

            string sqlStr4 = "DELETE [dbo].[UserAnylseNorms] WHERE Id = @Id";

            string sqlStr5 = "UPDATE [dbo].[LotteryAnalyseNorms]" +
                 "SET [LastStartPeriod] = @LastStartPeriod" +
                 ",[Enable] = @Enable" +
                 ",[ModifyTime] = GETDATE()" +
                 " WHERE [Id] = @Id";

            string lotteryAnalyseNormHashKey = string.Format(LsConstant.LotteryAnalyseNormRedisKey, lotteryType);

            using (var cn = LotteryDbConnection)
            {
                cn.Open();
                using (var trans = cn.BeginTransaction())
                {

                    try
                    {
                        // 1. 遍历更改的planIds
                        foreach (var planId in planIds)
                        {
                            var userAnylseNorm = cn.QuerySingleOrDefault<UserAnylseNorm>(sqlStr1, new
                            {
                                UserId = userId,
                                LotteryType = lotteryType.ToString(),
                                PlanId = planId,
                            },trans);
                            // 如果用户还没有添加过该计划
                            if (userAnylseNorm == null)
                            {
                                #region 用户没有添加过该计划

                                var lotteryAnalyseNorm = new LotteryAnalyseNorm()
                                {
                                    PlanId = planId,
                                    BasicHistoryCount = userBasicNorm.BasicHistoryCount,
                                    CreateUserId = userId,
                                    ForecastCount = userBasicNorm.ForecastCount,
                                    HotWeight = userBasicNorm.HotWeight,
                                    LastStartPeriod = 0, // Todo: set LastStartPeriod
                                    LotteryType = lotteryType.ToString(),
                                    UnitHistoryCount = userBasicNorm.UnitHistoryCount,
                                    Modulus = userBasicNorm.Modulus,
                                    OddEvenWeight = userBasicNorm.OddEvenWeight,
                                    MissingValueWeight = userBasicNorm.MissingValueWeight,
                                    ThreeRegionWeight = userBasicNorm.ThreeRegionWeight,
                                    SizeWeight = userBasicNorm.SizeWeight,
                                    PlanCycle = userBasicNorm.PlanCycle,

                                };

                                userAnylseNorm = new UserAnylseNorm()
                                {
                                    LotteryAnalyseNormId = lotteryAnalyseNorm.Id,
                                    LotteryType = lotteryType.ToString(),
                                    UserId = userId,
                                    PlanId = planId,

                                };
                                cn.Execute(sqlStr2_1, lotteryAnalyseNorm, trans);
                                cn.Execute(sqlStr2, userAnylseNorm, trans);

                                RedisHelper.SetHash(lotteryAnalyseNormHashKey, lotteryAnalyseNorm.Id,
                                      lotteryAnalyseNorm);
                                #endregion
                            }
                            else
                            {
                                #region 用户添加过该计划

                                var lotteryAnalyseNorm = cn.QuerySingle<LotteryAnalyseNorm>(sqlStr3, new
                                {
                                    Id = userAnylseNorm.LotteryAnalyseNormId
                                }, trans);
                                if (!lotteryAnalyseNorm.Enable)
                                {
                                    lotteryAnalyseNorm.Enable = true;
                                    lotteryAnalyseNorm.ModifyTime = DateTime.Now;
                                    lotteryAnalyseNorm.LastStartPeriod = 0;  // Todo: set LastStartPeriod

                                    cn.Execute(sqlStr5, new
                                    {
                                        lotteryAnalyseNorm.Id,
                                        lotteryAnalyseNorm.Enable,
                                        LatestStartPeriod = lotteryAnalyseNorm.LastStartPeriod
                                    }, trans);

                                    RedisHelper.SetHash(lotteryAnalyseNormHashKey, lotteryAnalyseNorm.Id,
                                        lotteryAnalyseNorm);
                                }

                                #endregion

                            }
                        }
                        // 2. 将旧计划且当前没有选的计划移除

                        #region 将旧计划且当前没有选的计划移除

                        foreach (var oldPlanId in userOldLotteryPlanIds)
                        {
                            if (planIds.Contains(oldPlanId))
                            {
                                continue;
                            }
                            var userAnylseNorm = cn.QuerySingleOrDefault<UserAnylseNorm>(sqlStr1, new
                            {
                                UserId = userId,
                                LotteryType = lotteryType.ToString(),
                                PlanId = oldPlanId,
                            },trans);

                            if (userAnylseNorm != null)
                            {
                                cn.Execute(sqlStr4, new
                                {
                                    Id = userAnylseNorm.Id
                                }, trans);

                                #region 修改LotteryAnalyseNorm 指标

                                var lotteryAnalyseNorm = cn.QuerySingle<LotteryAnalyseNorm>(sqlStr3, new
                                {
                                    Id = userAnylseNorm.LotteryAnalyseNormId
                                },trans);
                                if (lotteryAnalyseNorm.Enable)
                                {
                                    lotteryAnalyseNorm.Enable = false;
                                    lotteryAnalyseNorm.LastStartPeriod = 0;  // Todo: set LastStartPeriod
                                    
                                    cn.Execute(sqlStr5, new
                                    {
                                        lotteryAnalyseNorm.Id,
                                        lotteryAnalyseNorm.Enable,
                                        LatestStartPeriod = lotteryAnalyseNorm.LastStartPeriod
                                    }, trans);

                                    RedisHelper.Remove(lotteryAnalyseNormHashKey, lotteryAnalyseNorm.Id);
                                    
                                }

                                #endregion
                            }

                        }

                        #endregion

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {

                        LogDbHelper.LogError(ex, GetType().FullName + "UpdateUserPlans");
                        trans.Rollback();
                        throw ex;
                    }
                }
                
            }
        }


    }
}