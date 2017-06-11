namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserBasicNormEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserBasicNorms",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(maxLength: 50),
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
            DropTable("dbo.UserBasicNorms");
        }
    }
}
