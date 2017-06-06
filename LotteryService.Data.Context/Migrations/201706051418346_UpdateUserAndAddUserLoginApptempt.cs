namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserAndAddUserLoginApptempt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "App.UserLoginAttempts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false),
                        AccountName = c.String(nullable: false, maxLength: 50),
                        ClientIpAddress = c.String(nullable: false, maxLength: 50),
                        BrowserInfo = c.String(maxLength: 50),
                        LoginTime = c.DateTime(nullable: false),
                        LogoutTime = c.DateTime(),
                        LoginResult = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("App.UserLoginAttempts");
        }
    }
}
