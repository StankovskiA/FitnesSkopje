namespace FitnesSkopjeWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fav_GymName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Favourites", "gymName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Favourites", "gymName");
        }
    }
}
