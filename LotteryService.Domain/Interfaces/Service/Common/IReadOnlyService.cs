using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lottery.Entities;
using LotteryService.Common.Dependency;

namespace LotteryService.Domain.Interfaces.Service.Common
{
    public interface IReadOnlyService<TEntity> : ITransientDependency
              where TEntity : class
    {
        TEntity Get(string id);
        IEnumerable<TEntity> All();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}