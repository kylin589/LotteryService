namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LotteryPredictDataEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LotteryPredictDatas",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        NormId = c.String(),
                        CurrentPredictPeriod = c.Int(nullable: false),
                        StartPeriod = c.Int(nullable: false),
                        EndPeriod = c.Int(nullable: false),
                        MinorCycle = c.Int(nullable: false),
                        PredictedNum = c.String(),
                        PredictedResult = c.Int(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        ModifyTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LotteryPredictDatas");
        }
    }
}
