namespace FitnesSkopjeWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Tables");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        ActivationCode = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
    }
}
