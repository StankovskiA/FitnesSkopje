namespace FitnesSkopjeWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class virtualUserGymInReviews : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Reviews", "userId");
            CreateIndex("dbo.Reviews", "gymId");
            AddForeignKey("dbo.Reviews", "gymId", "dbo.Gyms", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Reviews", "userId", "dbo.Users", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "userId", "dbo.Users");
            DropForeignKey("dbo.Reviews", "gymId", "dbo.Gyms");
            DropIndex("dbo.Reviews", new[] { "gymId" });
            DropIndex("dbo.Reviews", new[] { "userId" });
        }
    }
}
