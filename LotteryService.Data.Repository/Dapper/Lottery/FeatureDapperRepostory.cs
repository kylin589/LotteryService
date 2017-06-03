using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lottery.Entities;
using LotteryService.Data.Repository.Dapper.Common;
using LotteryService.Domain.Interfaces.Repository.Common;

namespace LotteryService.Data.Repository.Dapper.Lottery
{
    public class FeatureDapperRepostory : DapperRepository,IDapperRepository<Feature>
    {
        public Feature Get(string id)
        {
            throw new NotImplementedException();
        }

        bool IDapperRepository<Feature>.Add(Feature entity)
        {
            throw new NotImplementedException();
        }

        public Feature Add(Feature entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Feature> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Feature> Find(Expression<Func<Feature, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Update(string id, object[] fields)
        {
            throw new NotImplementedException();
        }
    }
}