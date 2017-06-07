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
                        UserId = c.String(),
                        ApiAddress = c.String(maxLength: 50),
                        MethodName = c.String(maxLength: 10),
                        Parameters = c.String(maxLength: 500),
                        ExecutionTime = c.DateTime(nullable: false),
                        ExecutionDuration = c.Int(),
                        ClientIpAddress = c.String(),
                        ClientName = c.String(maxLength: 100),
                        BrowserInfo = c.String(maxLength: 100),
                        Exception = c.String(),
                        CustomData = c.String(maxLength: 500),
                        ActionName = c.String(maxLength: 50),
                        ControllerName = c.String(maxLength: 50),
                        IsExecSuccess = c.Boolean(),
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
                        Memo = c.String(),
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
                        Period = c.Int(nullable: false),
                        LotteryType = c.String(nullable: false, maxLength: 30),
                        Data = c.String(nullable: false),
                        InsertTime = c.DateTime(nullable: false),
                        LotteryDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "App.UserLoginAttempts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false),
                        TokenId = c.String(nullable: false, maxLength: 50),
                        AccountName = c.String(nullable: false, maxLength: 50),
                        ClientIpAddress = c.String(nullable: false, maxLength: 50),
                        BrowserInfo = c.String(maxLength: 50),
                        LoginTime = c.DateTime(nullable: false),
                        LogoutTime = c.DateTime(),
                        LoginResult = c.Int(nullable: false),
                        IsOnline = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "App.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(maxLength: 50),
                        Surname = c.String(maxLength: 50),
                        Password = c.String(nullable: false),
                        Email = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 14),
                        IsActive = c.Boolean(nullable: false),
                        LastLoginTime = c.DateTime(),
                        TokenId = c.String(maxLength: 100),
                        QQ = c.String(maxLength: 50),
                        Wechat = c.String(maxLength: 50),
                        WechatOpenId = c.String(maxLength: 100),
                        UserRegistType = c.Int(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.String(maxLength: 50),
                        CreatTime = c.DateTime(nullable: false),
                        ModifyTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("App.User");
            DropTable("App.UserLoginAttempts");
            DropTable("Lottery.LotteryDatas");
            DropTable("Lottery.Features");
            DropTable("Global.ErrorLogs");
            DropTable("Global.AuditLogs");
        }
    }
}
