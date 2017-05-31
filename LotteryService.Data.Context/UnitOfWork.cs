using System;
using LotteryService.Data.Context.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace LotteryService.Data.Context
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>,IDisposable
        where TContext : IDbContext, new()
    {
        private readonly ContextManager<TContext> _contextManager = new ContextManager<TContext>();

        private readonly IDbContext _dbContext;
        private bool _disposed;

        public UnitOfWork()
        {
           // _contextManager = ServiceLocator.Current.GetInstance<IContextManager<TContext>>() as ContextManager<TContext>;
            _dbContext = _contextManager.GetContext();
        }

        public void BeginTransaction()
        {
            _disposed = false;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }
    }
}