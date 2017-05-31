using System.Data.Entity.ModelConfiguration;
using Lottery.Entities;

namespace LotteryService.Data.Context.Mappings
{
    public class FeatureMap : EntityTypeConfiguration<Feature>
    {
        public FeatureMap()
        {
            Ignore(p => p.ValidationResult);
        }
    }
}