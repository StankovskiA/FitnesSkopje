namespace FitnesSkopjeWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gym_types : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GymTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "address");
            DropTable("dbo.GymTypes");
        }
    }
}
