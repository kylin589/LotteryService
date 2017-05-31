using System.Data.Entity.ModelConfiguration;
using Lottery.Entities;

namespace LotteryService.Data.Context.Mappings
{
    public class LotteryDataMap : EntityTypeConfiguration<LotteryData>
    {
        public LotteryDataMap()
        {
            Ignore(p => p.ValidationResult);
        }
    }
}