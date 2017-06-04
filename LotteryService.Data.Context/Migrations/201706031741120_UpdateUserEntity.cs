namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "UerName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "UerName", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
