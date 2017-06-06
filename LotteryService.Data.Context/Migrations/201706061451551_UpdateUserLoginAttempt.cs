namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserLoginAttempt : DbMigration
    {
        public override void Up()
        {
            AddColumn("App.UserLoginAttempts", "TokenId", c => c.String(nullable: false, maxLength: 50));
            AddColumn("App.UserLoginAttempts", "IsOnline", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("App.UserLoginAttempts", "IsOnline");
            DropColumn("App.UserLoginAttempts", "TokenId");
        }
    }
}
