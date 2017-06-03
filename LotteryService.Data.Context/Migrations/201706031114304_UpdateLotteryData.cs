namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLotteryData : DbMigration
    {
        public override void Up()
        {
            AddColumn("Global.ErrorLogs", "Memo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Global.ErrorLogs", "Memo");
        }
    }
}
