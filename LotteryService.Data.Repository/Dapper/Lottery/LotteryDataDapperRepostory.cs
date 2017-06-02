using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using Lottery.Entities;
using LotteryService.Data.Repository.Dapper.Common;
using LotteryService.Domain.Interfaces.Repository.Common;

namespace LotteryService.Data.Repository.Dapper.Lottery
{
    public class LotteryDataDapperRepostory : DapperRepository, IReadOnlyRepository<LotteryData>
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
            throw new NotImplementedException();
        }

        public LotteryData Get(string id)
        {
            throw new NotImplementedException();
        }
    }
}