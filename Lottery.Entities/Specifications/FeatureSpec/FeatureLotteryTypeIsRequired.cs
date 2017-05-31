using Lottery.Entities.Extend.Specification;

namespace Lottery.Entities.Specifications.FeatureSpec
{
    public class FeatureLotteryTypeIsRequired : ISpecification<Feature>
    {
        public bool IsSatisfiedBy(Feature entity)
        {
            return !string.IsNullOrEmpty(entity.LotteryType) && entity.LotteryType.Length > 0;
        }
    }
}