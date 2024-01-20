namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        number = c.Int(nullable: false, identity: true),
                        titleRU = c.String(),
                        titleEN = c.String(),
                        year = c.Int(nullable: false),
                        duration = c.Int(nullable: false),
                        country = c.String(),
                        genre = c.String(),
                        director = c.String(),
                        cast = c.String(),
                        rating = c.Double(nullable: false),
                        votes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.number);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Movies");
        }
    }
}
