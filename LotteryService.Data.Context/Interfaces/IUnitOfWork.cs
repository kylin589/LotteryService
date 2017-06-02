namespace LotteryService.Data.Context.Interfaces
{
    public interface IUnitOfWork<TDbContext>
         where TDbContext : IDbContext, new()
    {
        void BeginTransaction();

        bool SaveChanges();
    }
}