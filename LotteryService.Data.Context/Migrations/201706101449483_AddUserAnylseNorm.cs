namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserAnylseNorm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAnylseNorms",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        LotteryAnalyseNormId = c.String(maxLength: 128),
                        LotteryType = c.String(),
                        CreatTime = c.DateTime(nullable: false),
                        ModifyTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LotteryAnalyseNorms", t => t.LotteryAnalyseNormId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.LotteryAnalyseNormId);
            
            AddColumn("dbo.LotteryAnalyseNorms", "CreateUserId", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAnylseNorms", "UserId", "dbo.User");
            DropForeignKey("dbo.UserAnylseNorms", "LotteryAnalyseNormId", "dbo.LotteryAnalyseNorms");
            DropIndex("dbo.UserAnylseNorms", new[] { "LotteryAnalyseNormId" });
            DropIndex("dbo.UserAnylseNorms", new[] { "UserId" });
            DropColumn("dbo.LotteryAnalyseNorms", "CreateUserId");
            DropTable("dbo.UserAnylseNorms");
        }
    }
}
