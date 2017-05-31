namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Lottery.Features",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        LotteryType = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Lottery.LotteryDatas",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Period = c.Long(nullable: false),
                        LotteryType = c.String(nullable: false, maxLength: 30),
                        Data = c.String(nullable: false),
                        InsertTime = c.DateTime(nullable: false),
                        LotteryDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("Lottery.LotteryDatas");
            DropTable("Lottery.Features");
        }
    }
}
