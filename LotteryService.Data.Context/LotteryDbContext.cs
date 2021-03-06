﻿using System.Data.Entity;
using Lottery.Entities;
using LotteryService.Data.Context.Config;
using LotteryService.Data.Context.Migrations;

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

        public virtual IDbSet<LotteryData> LotteryDatas { get; set; }

        public virtual IDbSet<ErrorLog> ErrorLogs { get; set; }

        public virtual IDbSet<AuditLog> AuditLogs { get; set; }

        public virtual IDbSet<User> Users { get; set; }

        public virtual IDbSet<UserLoginAttempt> UserLoginAttempts { get; set; }

        public virtual IDbSet<LotteryConfig> LotteryConfigs { get; set; }

        public virtual IDbSet<LotteryAnalyseNorm> LotteryAnalyseNorms { get; set; }

        public virtual IDbSet<UserAnylseNorm> UserAnylseNorms { get; set; }

        public virtual IDbSet<UserBasicNorm> UserBasicNorms { get; set; }

        public virtual IDbSet<LotteryPredictData> LotteryPredictDatas { get; set; }
    }
}