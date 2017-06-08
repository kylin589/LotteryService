using Lottery.Entities.Extend.Specification;

namespace Lottery.Entities.Specifications.FeatureSpec
{
    public class FeatureLotteryTypeIsRequired : ISpecification<LotteryFeature>
    {
        public bool IsSatisfiedBy(LotteryFeature entity)
        {
            return !string.IsNullOrEmpty(entity.LotteryType) && entity.LotteryType.Length > 0;
        }
    }
}