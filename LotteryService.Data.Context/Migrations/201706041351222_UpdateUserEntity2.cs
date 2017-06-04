namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserEntity2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "User");
            MoveTable(name: "dbo.User", newSchema: "Application");
        }
        
        public override void Down()
        {
            MoveTable(name: "Application.User", newSchema: "dbo");
            RenameTable(name: "dbo.User", newName: "Users");
        }
    }
}
