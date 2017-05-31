using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Lottery.Entities;
using LotteryService.Data.Context.Config;
using LotteryService.Data.Context.Mappings;

namespace LotteryService.Data.Context
{
    public class LotteryDbContext : BaseDbContext
    {
        public LotteryDbContext() 
            : base("Default")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new LotteryDataMap());
            modelBuilder.Configurations.Add(new FeatureMap());
        }

        public virtual IDbSet<Feature> Features { get; set; }

        public virtual IDbSet<LotteryData> LotteryDatas { get; set; }
    }
}