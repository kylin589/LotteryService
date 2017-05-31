using Lottery.Entities.Extend.Validation;

namespace Lottery.Entities.Extend.Interfaces.Validation
{
    public interface ISelfValidation
    {
        ValidationResult ValidationResult { get; }

        bool IsValid { get; }
    }
}