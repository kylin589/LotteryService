using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LotteryService.Application.Interfaces
{
    /// <summary>
    /// 包含读写操作
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IAppService<TEntity> : IWriteOnlyAppService<TEntity>, IDisposable
         where TEntity : class
    {
        TEntity Get(int id, bool @readonly = false);
        IEnumerable<TEntity> All(bool @readonly = false);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = false);
    }
}