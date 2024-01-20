namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    
    public partial class Migration5 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Movies");
            AlterColumn("dbo.Movies", "№", c => c.Int(nullable: false));
            AlterColumn("dbo.Movies", "Название на русском", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Movies", new[] { "Название на русском", "Год выпуска" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Movies");
            AlterColumn("dbo.Movies", "Название на русском", c => c.String());
            AlterColumn("dbo.Movies", "№", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Movies", "№");
        }
    }
}
