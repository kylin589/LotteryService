using Lottery.Entities.Extend.Validation;
using Lottery.Entities.Specifications.FeatureSpec;

namespace Lottery.Entities.Validations
{
    public class FeatureIsValidValidation : Validation<Feature>
    {
        public FeatureIsValidValidation()
        {
            base.AddRule(new ValidationRule<Feature>(new FeatureLotteryTypeIsRequired(), ValidationMessages.LotteryTypeIsRequired));
        }
    }
}