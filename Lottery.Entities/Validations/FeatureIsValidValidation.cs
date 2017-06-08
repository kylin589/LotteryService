using Lottery.Entities.Extend.Validation;
using Lottery.Entities.Specifications.FeatureSpec;

namespace Lottery.Entities.Validations
{
    public class FeatureIsValidValidation : Validation<LotteryFeature>
    {
        public FeatureIsValidValidation()
        {
            base.AddRule(new ValidationRule<LotteryFeature>(new FeatureLotteryTypeIsRequired(), ValidationMessages.LotteryTypeIsRequired));
        }
    }
}