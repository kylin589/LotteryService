using System;
using LotteryService.Data.Context.Interfaces;
using System.Web;

namespace LotteryService.Data.Context
{
    public class ContextManager<TContext> : IContextManager<TContext>
         where TContext : IDbContext, new()
    {
        private readonly string ContextKey = "ContextManager.Context";

        private TContext _dbContext;

        public ContextManager()
        {
            ContextKey = "ContextKey." + typeof(TContext).Name;
        }


        public void Finish()
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                if (context.Items[ContextKey] != null)
                    ((IDbContext)context.Items[ContextKey]).Dispose();
            }

            //if (_dbContext != null)
            //{
            //    _dbContext.Dispose();
            //}

        }

        public IDbContext GetContext()
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                if (context.Items[ContextKey] == null)
                {
                    _dbContext = new TContext();
                    context.Items[ContextKey] = _dbContext;
                }
                return context.Items[ContextKey] as IDbContext;
            }
            if (_dbContext == null)
            {
                _dbContext = new TContext();
            }
            return _dbContext;
        }
    }
}