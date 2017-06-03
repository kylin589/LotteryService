using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lottery.Entities;
using LotteryService.Common.Dependency;

namespace LotteryService.Domain.Interfaces.Repository.Common
{
    public interface IDapperRepository<TEntity> : ITransientDependency
         where TEntity : class
    {
        TEntity Get(string id);

        bool Add(TEntity entity);

        IEnumerable<TEntity> All();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        bool Update(string id, object[] fields);
    }
}