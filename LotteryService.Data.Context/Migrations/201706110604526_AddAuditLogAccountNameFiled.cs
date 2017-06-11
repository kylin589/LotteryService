namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditLogAccountNameFiled : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuditLogs", "AccountName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AuditLogs", "AccountName");
        }
    }
}
