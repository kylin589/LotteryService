namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserAnylseNormFiledPlanId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAnylseNorms", "PlanId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserAnylseNorms", "PlanId");
        }
    }
}
