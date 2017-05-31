namespace Lottery.Entities.Extend.Interfaces.Validation
{
    public interface IValidationRule<in TEntity>
    {
        string ErrorMessage { get; }

        bool Valid(TEntity entity);
    }
}