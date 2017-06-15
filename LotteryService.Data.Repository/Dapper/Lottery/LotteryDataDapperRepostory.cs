using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Dapper;
using Lottery.Entities;
using LotteryService.Common.Enums;
using LotteryService.Common.Tools;
using LotteryService.Data.Repository.Dapper.Common;
using LotteryService.Domain.Interfaces.Repository.Common;
using LotteryService.Domain.Interfaces.Repository.Dapper;

namespace LotteryService.Data.Repository.Dapper.Lottery
{
    public class LotteryDataDapperRepostory : DapperRepository, ILotteryDataDapperRepository
    {

        public IEnumerable<LotteryData> All()
        {
            using (var cn = LotteryDbConnection)
            {
                var lotteryDatas = cn.Query<LotteryData>(
                    "SELECT Period ,LotteryType ,Data ,LotteryDateTime" +
                    " FROM dbo.LotteryDatas").ToList();
                return lotteryDatas;
            }
        }

        public IEnumerable<LotteryData> Find(Expression<Func<LotteryData, bool>> predicate)
        {
            using (var cn = LotteryDbConnection)
            {
                throw new NotImplementedException();
            }
        }

        public bool Add(LotteryData entity)
        {
            using (var cn = LotteryDbConnection)
            {
                var sqlStr = "INSERT dbo.LotteryDatas(Id,Period,LotteryType,Data,InsertTime,LotteryDateTime)" +
                "VALUES(@Id,@Period,@LotteryType,@Data,GETDATE(),@LotteryDateTime)";
                return cn.Execute(sqlStr, new
                {
                    entity.Id,
                    entity.Period,
                    entity.Data,
                    entity.InsertTime,
                    entity.LotteryDateTime,
                    entity.LotteryType,
                }) > 0;
            }
        }

        public bool Update(string id, object[] fields)
        {
            throw new NotImplementedException();
        }

        public bool ExsitData(string lotteryType, int period)
        {
            using (var cn = LotteryDbConnection)
            {
                var lotteryData = cn.QuerySingleOrDefault<LotteryData>(
                    " SELECT  Id , Period , LotteryType , Data , InsertTime , LotteryDateTime" +
                    " FROM dbo.LotteryDatas WHERE Period = @Period AND LotteryType=@LotteryType ",
                    new
                    {
                        LotteryType = lotteryType,
                        Period = period
                    });
                if (lotteryData == null)
                {
                    return false;
                }
                return true;
            }
        }

        public LotteryData GetLatestLotteryData(string lotteryType)
        {
            using (var cn = LotteryDbConnection)
            {
                var lotteryData = cn.Query<LotteryData>(
                    "SELECT TOP 1  Id , Period , LotteryType , Data , InsertTime , LotteryDateTime " +
                    "FROM dbo.LotteryDatas WHERE LotteryType=@LotteryType ORDER BY Period DESC",
                    new
                    {
                        LotteryType = lotteryType
                    }).SingleOrDefault();
                return lotteryData;
            }
        }

        public LotteryData GetLotteryData(string lotteryType, int? peroiod)
        {
            if (peroiod.HasValue)
            {
                var sqlStr1 = " SELECT * FROM dbo.[LotteryDatas] WHERE Period=@Period AND LotteryType=@LotteryType";
                using (var cn = LotteryDbConnection)
                {
                    var lotteryData = cn.Query<LotteryData>(sqlStr1,
                    new
                    {
                        LotteryType = lotteryType,
                        Period = peroiod
                    }).SingleOrDefault();
                    return lotteryData;
                }
            }
            return GetLatestLotteryData(lotteryType);
        }

        public IList<LotteryData> GetLotteryDatas(string lotteryType, int pageIndex, int pageSize,out int totalCount)
        {
            //var sqlStr1 = "SELECT * FROM dbo.LotteryDatas WHERE LotteryType =N'@LotteryType' ORDER BY Period";

            var sqlStr1 = SqlParser.PageSql("dbo.LotteryDatas", "AND LotteryType =@LotteryType", "Id", "Period",pageSize,pageIndex,OrderType.Desc);
            var sqlStr2 = SqlParser.TotalCount("dbo.LotteryDatas", "AND LotteryType =@LotteryType");
       
            using (var cn = LotteryDbConnection)
            {
                var list = cn.Query<LotteryData>(sqlStr1, new
                {
                    LotteryType = lotteryType
                }).ToList();
                totalCount = cn.ExecuteScalar<int>(sqlStr2, new
                {
                    LotteryType = lotteryType
                });
                return list;
            }
        }

        public IDictionary<LotteryType, IList<LotteryData>> GetAnaylesBasicLotteryDatas(int basicHistoryCount)
        {
            IDictionary<LotteryType, IList<LotteryData>> _dictionary = new Dictionary<LotteryType, IList<LotteryData>>();
            using (var cn = LotteryDbConnection)
            {
                string sqlStr1 = "SELECT LotteryType FROM dbo.LotteryDatas GROUP BY LotteryType";
                string sqlStr2 = "SELECT TOP "+ basicHistoryCount +" * FROM dbo.LotteryDatas WHERE LotteryType = @LotteryType ORDER BY Period DESC";
                var lotteryTypes = cn.Query<string>(sqlStr1);

                foreach (var lotteryType in lotteryTypes)
                {
                    var lotteryAnalysDatas = cn.Query<LotteryData>(sqlStr2, new
                    {
                        LotteryType = lotteryType,
                    }).ToList();
                    _dictionary.Add(Utils.StringConvertEnum<LotteryType>(lotteryType), lotteryAnalysDatas);
                }
            }
            return _dictionary;
        }

        public LotteryData Get(string id)
        {
            using (var cn = LotteryDbConnection)
            {
                var lotteryData = cn.Query<LotteryData>(
                    "SELECT TOP 1  Id , Period , LotteryType , Data , InsertTime , LotteryDateTime " +
                    "FROM dbo.LotteryDatas WHERE Id=@Id",
                    new
                    {
                        Id = id
                    }).SingleOrDefault();
                return lotteryData;
            }
        }
    }
}