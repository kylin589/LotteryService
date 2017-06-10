namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLotteryAnalyseNorm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LotteryAnalyseNorms",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PlanId = c.Int(nullable: false),
                        PlanCycle = c.Int(nullable: false),
                        LatestStartPeriod = c.Int(nullable: false),
                        ForecastCount = c.Int(nullable: false),
                        BasicHistoryCount = c.Int(nullable: false),
                        UnitHistoryCount = c.Int(nullable: false),
                        HotWeight = c.Int(nullable: false),
                        SizeWeight = c.Int(nullable: false),
                        ThreeRegionWeight = c.Int(nullable: false),
                        MissingValueWeight = c.Int(nullable: false),
                        OddEvenWeight = c.Int(nullable: false),
                        Modulus = c.String(maxLength: 50),
                        LotteryType = c.String(nullable: false, maxLength: 20),
                        Enable = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        ModifyTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LotteryAnalyseNorms");
        }
    }
}
