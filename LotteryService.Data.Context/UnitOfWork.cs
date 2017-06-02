using System;
using LotteryService.Data.Context.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace LotteryService.Data.Context
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>,IDisposable
        where TContext : IDbContext, new()
    {
        private readonly IContextManager<TContext> _contextManager;

        private readonly IDbContext _dbContext;
        private bool _disposed;

        public UnitOfWork()
        {
           _contextManager = ServiceLocator.Current.GetInstance<IContextManager<TContext>>();
           _dbContext = _contextManager.GetContext();
        }

        public void BeginTransaction()
        {
            _disposed = false;
        }

        public bool SaveChanges()
        {
            return _dbContext.SaveChanges() > 0;
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