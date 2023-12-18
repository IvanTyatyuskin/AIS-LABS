namespace LAB6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lab7_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProcessorManufacturers",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Smartphones", "processorManufacturerID", c => c.Guid());
            CreateIndex("dbo.Smartphones", "processorManufacturerID");
            AddForeignKey("dbo.Smartphones", "processorManufacturerID", "dbo.ProcessorManufacturers", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Smartphones", "processorManufacturerID", "dbo.ProcessorManufacturers");
            DropIndex("dbo.Smartphones", new[] { "processorManufacturerID" });
            DropColumn("dbo.Smartphones", "processorManufacturerID");
            DropTable("dbo.ProcessorManufacturers");
        }
    }
}
