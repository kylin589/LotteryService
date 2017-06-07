namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserLoginAttemptTokenIdRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("App.UserLoginAttempts", "TokenId", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("App.UserLoginAttempts", "TokenId", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
