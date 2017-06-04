namespace LotteryService.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UerName = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(maxLength: 50),
                        Password = c.String(nullable: false),
                        Email = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 14),
                        IsActive = c.Boolean(nullable: false),
                        LastLoginTime = c.DateTime(),
                        SessionId = c.String(maxLength: 100),
                        QQ = c.String(maxLength: 50),
                        Wechat = c.String(maxLength: 50),
                        WechatOpenId = c.String(maxLength: 100),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.String(maxLength: 50),
                        CreatTime = c.DateTime(nullable: false),
                        ModifyTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
