namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLotteryDataPeriodFiled : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Lottery.LotteryDatas", "Period", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("Lottery.LotteryDatas", "Period", c => c.Long(nullable: false));
        }
    }
}
