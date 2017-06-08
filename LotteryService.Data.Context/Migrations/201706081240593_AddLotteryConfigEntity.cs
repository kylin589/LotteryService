namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLotteryConfigEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LotteryConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LotteryType = c.String(nullable: false, maxLength: 10),
                        ConfigData = c.String(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Features");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        LotteryType = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.LotteryConfigs");
        }
    }
}
