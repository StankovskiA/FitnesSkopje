namespace FitnesSkopjeWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Data : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.FitnessCenters");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FitnessCenters",
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
    }
}
