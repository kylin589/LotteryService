using Lottery.Entities.Extend.Interfaces.Validation;
using Lottery.Entities.Extend.Specification;

namespace Lottery.Entities.Extend.Validation
{
    public class ValidationRule<TEntity> : IValidationRule<TEntity>
        where TEntity : class 
    {
        private readonly ISpecification<TEntity> _specificationRule;

        public ValidationRule(ISpecification<TEntity> specificationRule,string errorMessage)
        {
            _specificationRule = specificationRule;
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }

        public bool Valid(TEntity entity)
        {
            return _specificationRule.IsSatisfiedBy(entity);
        }
    }
}