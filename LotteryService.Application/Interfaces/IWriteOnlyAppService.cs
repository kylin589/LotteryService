using Lottery.Entities.Extend.Validation;

namespace LotteryService.Application.Interfaces
{
    public interface IWriteOnlyAppService<in TEntity>
        where TEntity : class
    {
        ValidationResult Create(TEntity orderDetail);
        ValidationResult Update(TEntity orderDetail);
        ValidationResult Remove(TEntity orderDetail);
    }
}