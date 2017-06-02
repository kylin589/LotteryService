using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Lottery.Entities;
using LotteryService.Common.Dependency;

namespace LotteryService.Domain.Interfaces.Repository.Common
{
    public interface IRepository<TEntity> : ITransientDependency
    where TEntity : class 
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        TEntity Get(string id);

        IEnumerable<TEntity> All(bool @readonly = false);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = false);
        
    }
}