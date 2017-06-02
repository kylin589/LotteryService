namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditLogFileds : DbMigration
    {
        public override void Up()
        {
            AddColumn("Global.AuditLogs", "ApiAddress", c => c.String(maxLength: 50));
            AddColumn("Global.AuditLogs", "ActionName", c => c.String(maxLength: 50));
            AddColumn("Global.AuditLogs", "ControllerName", c => c.String(maxLength: 50));
            DropColumn("Global.AuditLogs", "RequestAddress");
        }
        
        public override void Down()
        {
            AddColumn("Global.AuditLogs", "RequestAddress", c => c.String(maxLength: 50));
            DropColumn("Global.AuditLogs", "ControllerName");
            DropColumn("Global.AuditLogs", "ActionName");
            DropColumn("Global.AuditLogs", "ApiAddress");
        }
    }
}
