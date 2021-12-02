namespace FitnesSkopjeWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Coordinates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Gyms", "Latitude", c => c.Double());
            AlterColumn("dbo.Gyms", "Longitude", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Gyms", "Longitude", c => c.Single());
            AlterColumn("dbo.Gyms", "Latitude", c => c.Single());
        }
    }
}
