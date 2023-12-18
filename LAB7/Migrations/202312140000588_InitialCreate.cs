namespace LAB6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Smartphones",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Processor = c.String(),
                        Ram = c.String(),
                        Storage = c.String(),
                        Price = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Smartphones");
        }
    }
}
