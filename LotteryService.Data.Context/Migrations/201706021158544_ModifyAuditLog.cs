namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyAuditLog : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Global.AuditLogs", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("Global.AuditLogs", "UserId", c => c.Long());
        }
    }
}
