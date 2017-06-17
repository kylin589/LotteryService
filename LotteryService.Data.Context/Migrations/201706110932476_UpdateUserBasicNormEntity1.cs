namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserBasicNormEntity1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserBasicNorms", "LastStartPeriod", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserBasicNorms", "LastStartPeriod", c => c.Int(nullable: false));
        }
    }
}
