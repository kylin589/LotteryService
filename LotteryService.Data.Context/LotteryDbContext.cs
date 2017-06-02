using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Lottery.Entities;
using LotteryService.Data.Context.Config;

namespace LotteryService.Data.Context
{
    public class LotteryDbContext : BaseDbContext
    {
        static LotteryDbContext()  // runs once
        {
            Database.SetInitializer<LotteryDbContext>(new CreateDatabaseIfNotExists<LotteryDbContext>());
        }

        public LotteryDbContext()
                : base("Default")
        {

        }

        public virtual IDbSet<Feature> Features { get; set; }

        public virtual IDbSet<LotteryData> LotteryDatas { get; set; }

        public virtual IDbSet<ErrorLog> ErrorLogs { get; set; }

        public virtual IDbSet<AuditLog> AuditLogs { get; set; }
    }
}