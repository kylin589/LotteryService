using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Lottery.Entities;
using LotteryService.Data.Context.Interfaces;

namespace LotteryService.Data.Context.Config
{
    public abstract class BaseDbContext : DbContext,IDbContext
    {
        protected BaseDbContext(string connectionStringName, int? currentUserId = null)
            : base(connectionStringName)
        {
            CurrentUserId = currentUserId;
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public int? CurrentUserId { get; private set; }
    }
}