namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLotteryAnalyseNormEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LotteryAnalyseNorms", "LastStartPeriod", c => c.Int(nullable: false));
            AddColumn("dbo.LotteryAnalyseNorms", "CurrentAccuracy", c => c.Double());
            DropColumn("dbo.LotteryAnalyseNorms", "LatestStartPeriod");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LotteryAnalyseNorms", "LatestStartPeriod", c => c.Int(nullable: false));
            DropColumn("dbo.LotteryAnalyseNorms", "CurrentAccuracy");
            DropColumn("dbo.LotteryAnalyseNorms", "LastStartPeriod");
        }
    }
}
