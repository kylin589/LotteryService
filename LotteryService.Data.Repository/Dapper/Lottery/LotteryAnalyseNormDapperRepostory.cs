using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Lottery.Entities;
using LotteryService.Common.Enums;
using LotteryService.Common.Tools;
using LotteryService.Data.Repository.Dapper.Common;
using LotteryService.Domain.Interfaces.Repository.Dapper;

namespace LotteryService.Data.Repository.Dapper.Lottery
{
    public class LotteryAnalyseNormDapperRepostory : DapperRepository, ILotteryAnalyseNormDapperRepostory
    {
        public IDictionary<LotteryType, IList<LotteryAnalyseNorm>> GetAll()
        {
            IDictionary<LotteryType, IList<LotteryAnalyseNorm>> _dictionary = new Dictionary<LotteryType, IList<LotteryAnalyseNorm>>();
            using (var cn = LotteryDbConnection)
            {
                string sqlStr1 = "SELECT LotteryType FROM dbo.LotteryAnalyseNorms GROUP BY LotteryType";
                string sqlStr2 = "SELECT * FROM dbo.LotteryAnalyseNorms WHERE LotteryType = @LotteryType";
                var lotteryTypes = cn.Query<string>(sqlStr1);

                foreach (var lotteryType in lotteryTypes)
                {
                    var lotteryAnalyseNorms = cn.Query<LotteryAnalyseNorm>(sqlStr2, new
                    {
                        LotteryType = lotteryType
                    }).ToList();
                    _dictionary.Add(Utils.StringConvertEnum<LotteryType>(lotteryType), lotteryAnalyseNorms);
                }
            }
            return _dictionary;
        }

        public UserBasicNorm GetUserBasicNorm(string userId, LotteryType lotteryType)
        {
            UserBasicNorm userBasicNorm = null;
            using (var cn = LotteryDbConnection)
            {
                string sqlStr1 =
                    "SELECT TOP 1 * FROM [Lottery-Dev].[dbo].[UserBasicNorms] WHERE UserId=@UserId AND LotteryType=@LotteryType";

                string sqlStr2 =
                   "SELECT TOP 1 * FROM [Lottery-Dev].[dbo].[UserBasicNorms] WHERE IsDefault=@IsDefault AND LotteryType=@LotteryType";
                userBasicNorm = cn.QuerySingleOrDefault<UserBasicNorm>(sqlStr1, new
                {
                    UserId = userId,
                    LotteryType = lotteryType.ToString()
                }) ?? cn.QuerySingleOrDefault<UserBasicNorm>(sqlStr2, new
                {
                    IsDefault = 1,
                    LotteryType = lotteryType.ToString()
                });
            }
            return userBasicNorm;
        }

        public bool SetUserBasicNorm(UserBasicNorm userBasicNorm)
        {
            bool isSuccess = false;
            using (var cn = LotteryDbConnection)
            {
                string sqlStr1 =
                   "SELECT TOP 1 * FROM [Lottery-Dev].[dbo].[UserBasicNorms] WITH(NOLOCK) WHERE UserId=@UserId AND LotteryType=@LotteryType";
                string sqlStr2 =
                    "INSERT INTO [dbo].[UserBasicNorms]([Id] ,[UserId],[PlanCycle],[ForecastCount],[BasicHistoryCount] ,[UnitHistoryCount],[HotWeight],[SizeWeight] ,[ThreeRegionWeight]," +
                    " [MissingValueWeight],[OddEvenWeight],[Modulus],[LotteryType],[LastStartPeriod],[Enable],[IsDefault],[CreatTime],[ModifyTime])" +
                    " VALUES(@Id,@UserId,@PlanCycle,@ForecastCount,@BasicHistoryCount,@UnitHistoryCount,@HotWeight,@SizeWeight,@ThreeRegionWeight,@MissingValueWeight,@OddEvenWeight,@Modulus,@LotteryType,@LastStartPeriod,@Enable," +
                    " @IsDefault,GETDATE(),GETDATE())";

                string sqlStr3 = "UPDATE [dbo].[UserBasicNorms]" +
                                 "SET[PlanCycle] = @PlanCycle" +
                                 ",[LastStartPeriod] = @LastStartPeriod" +
                                 ",[ForecastCount] = @ForecastCount" +
                                 ",[BasicHistoryCount] = @BasicHistoryCount" +
                                 ",[UnitHistoryCount] = @UnitHistoryCount" +
                                 ",[HotWeight] = @HotWeight" +
                                 ",[SizeWeight] = @SizeWeight" +
                                 ",[ThreeRegionWeight] = @ThreeRegionWeight" +
                                 ",[MissingValueWeight] = @MissingValueWeight" +
                                 ",[OddEvenWeight] =@OddEvenWeight" +
                                 ",[Modulus] = @Modulus " +
                                 ",[ModifyTime] = GETDATE()" +
                                 " WHERE[UserId] = @UserId AND LotteryType = @LotteryType";

                var userBasicNormExist = cn.QuerySingleOrDefault(sqlStr1, new
                {
                    userBasicNorm.UserId,
                    userBasicNorm.LotteryType
                });
                if (userBasicNormExist == null)
                {
                    isSuccess = cn.Execute(sqlStr2, userBasicNorm) > 0;
                }
                else
                {
                    isSuccess = cn.Execute(sqlStr3, userBasicNorm) > 0;
                }

            }
            return isSuccess;
        }

        public IDictionary<LotteryType, IList<LotteryAnalyseNorm>> GetAllEnable()
        {
            IDictionary<LotteryType, IList<LotteryAnalyseNorm>> _dictionary = new Dictionary<LotteryType, IList<LotteryAnalyseNorm>>();
            using (var cn = LotteryDbConnection)
            {
                string sqlStr1 = "SELECT LotteryType FROM dbo.LotteryAnalyseNorms GROUP BY LotteryType";
                string sqlStr2 = "SELECT * FROM dbo.LotteryAnalyseNorms WHERE LotteryType = @LotteryType AND　Enable = @Enable";
                var lotteryTypes = cn.Query<string>(sqlStr1);

                foreach (var lotteryType in lotteryTypes)
                {
                    var lotteryAnalyseNorms = cn.Query<LotteryAnalyseNorm>(sqlStr2, new
                    {
                        LotteryType = lotteryType,
                        Enable = 1
                    }).ToList();
                    _dictionary.Add(Utils.StringConvertEnum<LotteryType>(lotteryType), lotteryAnalyseNorms);
                }
            }
            return _dictionary;
        }
    }
}