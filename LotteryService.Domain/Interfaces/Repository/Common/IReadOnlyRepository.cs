using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lottery.Entities;

namespace LotteryService.Domain.Interfaces.Repository.Common
{
    public interface IReadOnlyRepository<TEntity> 
         where TEntity : class
    {
        TEntity Get(string id);

        IEnumerable<TEntity> All();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}