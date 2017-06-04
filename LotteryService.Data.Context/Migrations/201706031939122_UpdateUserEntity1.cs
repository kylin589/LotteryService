namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserEntity1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserName", c => c.String(maxLength: 50));
            AddColumn("dbo.Users", "UserRegistType", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "UerName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UerName", c => c.String(maxLength: 50));
            DropColumn("dbo.Users", "UserRegistType");
            DropColumn("dbo.Users", "UserName");
        }
    }
}
