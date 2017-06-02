namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditLogFileds1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Global.AuditLogs", "IsExecSuccess", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("Global.AuditLogs", "IsExecSuccess");
        }
    }
}
