using Lottery.Entities.Extend.Validation;

namespace Lottery.Entities.Extend.Interfaces.Validation
{
    public interface IValidation<in TEntity>
    {
        ValidationResult Valid(TEntity entity);
    }
}