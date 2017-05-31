using LotteryService.Data.Context.Interfaces;

namespace LotteryService.Application.Interfaces
{
    public interface ITransactionAppService<TContext>
         where TContext : IDbContext, new()
    {
        void BeginTransaction();
        void Commit();
    }
}