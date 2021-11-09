namespace FitnesSkopjeWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gyms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Number = c.String(),
                        WorkingTime = c.String(),
                        Areas = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Gyms");
        }
    }
}
