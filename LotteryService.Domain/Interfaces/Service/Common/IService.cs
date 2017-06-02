using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lottery.Entities;
using Lottery.Entities.Extend.Validation;
using LotteryService.Common.Dependency;

namespace LotteryService.Domain.Interfaces.Service.Common
{
    public interface IService<TEntity> : ITransientDependency
          where TEntity : class
    {
        TEntity Get(string id, bool @readonly = false);
        IEnumerable<TEntity> All(bool @readonly = false);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = false);

        ValidationResult Add(TEntity department);

        ValidationResult Update(TEntity department);

        ValidationResult Delete(TEntity entity);
    }
}