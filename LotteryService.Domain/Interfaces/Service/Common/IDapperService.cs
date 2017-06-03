using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lottery.Entities;
using Lottery.Entities.Extend.Validation;
using LotteryService.Common.Dependency;

namespace LotteryService.Domain.Interfaces.Service.Common
{
    public interface IDapperService<TEntity> : ITransientDependency
              where TEntity : class
    {
        TEntity Get(string id);

        IEnumerable<TEntity> All();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        ValidationResult Add(TEntity entity);

        ValidationResult Update(string id, params object[] fields);
    }
}