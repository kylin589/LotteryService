namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveEntitySchemaAddIndex : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "Global.AuditLogs", newSchema: "dbo");
            MoveTable(name: "Global.ErrorLogs", newSchema: "dbo");
            MoveTable(name: "Lottery.Features", newSchema: "dbo");
            MoveTable(name: "Lottery.LotteryDatas", newSchema: "dbo");
            MoveTable(name: "App.UserLoginAttempts", newSchema: "dbo");
            MoveTable(name: "App.User", newSchema: "dbo");
            CreateIndex("dbo.LotteryDatas", "Period");
            CreateIndex("dbo.User", "UserName");
            CreateIndex("dbo.User", "Email");
            CreateIndex("dbo.User", "Phone");
            CreateIndex("dbo.User", "TokenId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", new[] { "TokenId" });
            DropIndex("dbo.User", new[] { "Phone" });
            DropIndex("dbo.User", new[] { "Email" });
            DropIndex("dbo.User", new[] { "UserName" });
            DropIndex("dbo.LotteryDatas", new[] { "Period" });
            MoveTable(name: "dbo.User", newSchema: "App");
            MoveTable(name: "dbo.UserLoginAttempts", newSchema: "App");
            MoveTable(name: "dbo.LotteryDatas", newSchema: "Lottery");
            MoveTable(name: "dbo.Features", newSchema: "Lottery");
            MoveTable(name: "dbo.ErrorLogs", newSchema: "Global");
            MoveTable(name: "dbo.AuditLogs", newSchema: "Global");
        }
    }
}
