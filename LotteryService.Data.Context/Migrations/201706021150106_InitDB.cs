namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Global.AuditLogs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.Long(),
                        RequestAddress = c.String(maxLength: 50),
                        MethodName = c.String(maxLength: 10),
                        Parameters = c.String(maxLength: 500),
                        ExecutionTime = c.DateTime(nullable: false),
                        ExecutionDuration = c.Int(),
                        ClientIpAddress = c.String(),
                        ClientName = c.String(maxLength: 100),
                        BrowserInfo = c.String(maxLength: 100),
                        Exception = c.String(),
                        CustomData = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Global.ErrorLogs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(),
                        Thread = c.String(),
                        Level = c.String(),
                        OperationType = c.String(),
                        IP = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        Logger = c.String(),
                        MethodName = c.String(),
                        Message = c.String(),
                        Exception = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Lottery.Features",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        LotteryType = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Lottery.LotteryDatas",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
            DropTable("Global.ErrorLogs");
            DropTable("Global.AuditLogs");
        }
    }
}
