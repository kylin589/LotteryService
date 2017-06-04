using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Dapper;
using Lottery.Entities;
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
                    " FROM Lottery.LotteryDatas").ToList();
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
                var sqlStr = "INSERT Lottery.LotteryDatas(Id,Period,LotteryType,Data,InsertTime,LotteryDateTime)" +
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
                    " FROM Lottery.LotteryDatas WHERE Period = @Period AND LotteryType=@LotteryType ",
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
                    "FROM Lottery.LotteryDatas WHERE LotteryType=@LotteryType ORDER BY Period DESC",
                    new
                    {
                        LotteryType = lotteryType
                    }).SingleOrDefault();
                return lotteryData;
            }
        }

        public LotteryData Get(string id)
        {
            using (var cn = LotteryDbConnection)
            {
                var lotteryData = cn.Query<LotteryData>(
                    "SELECT TOP 1  Id , Period , LotteryType , Data , InsertTime , LotteryDateTime " +
                    "FROM Lottery.LotteryDatas WHERE Id=@Id",
                    new
                    {
                        Id = id
                    }).SingleOrDefault();
                return lotteryData;
            }
        }
    }
}