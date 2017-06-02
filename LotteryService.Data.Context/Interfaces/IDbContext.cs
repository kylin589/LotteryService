using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Lottery.Entities;

namespace LotteryService.Data.Context.Interfaces
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();

        void Dispose();

        int? CurrentUserId { get; }
    }
}