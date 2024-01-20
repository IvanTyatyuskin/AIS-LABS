namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        countryId = c.Int(nullable: false, identity: true),
                        Странапроизводства = c.String(name: "Страна производства"),
                    })
                .PrimaryKey(t => t.countryId);
            
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        directorId = c.Int(nullable: false, identity: true),
                        Режиссер = c.String(),
                    })
                .PrimaryKey(t => t.directorId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        genreId = c.Int(nullable: false, identity: true),
                        Жанр = c.String(),
                    })
                .PrimaryKey(t => t.genreId);
            
            AddColumn("dbo.Movies", "countryId", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "genreId", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "directorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Movies", "countryId");
            CreateIndex("dbo.Movies", "genreId");
            CreateIndex("dbo.Movies", "directorId");
            AddForeignKey("dbo.Movies", "countryId", "dbo.Countries", "countryId", cascadeDelete: true);
            AddForeignKey("dbo.Movies", "directorId", "dbo.Directors", "directorId", cascadeDelete: true);
            AddForeignKey("dbo.Movies", "genreId", "dbo.Genres", "genreId", cascadeDelete: true);
            DropColumn("dbo.Movies", "Страна производства");
            DropColumn("dbo.Movies", "Жанр");
            DropColumn("dbo.Movies", "Режиссер");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Режиссер", c => c.String());
            AddColumn("dbo.Movies", "Жанр", c => c.String());
            AddColumn("dbo.Movies", "Страна производства", c => c.String());
            DropForeignKey("dbo.Movies", "genreId", "dbo.Genres");
            DropForeignKey("dbo.Movies", "directorId", "dbo.Directors");
            DropForeignKey("dbo.Movies", "countryId", "dbo.Countries");
            DropIndex("dbo.Movies", new[] { "directorId" });
            DropIndex("dbo.Movies", new[] { "genreId" });
            DropIndex("dbo.Movies", new[] { "countryId" });
            DropColumn("dbo.Movies", "directorId");
            DropColumn("dbo.Movies", "genreId");
            DropColumn("dbo.Movies", "countryId");
            DropTable("dbo.Genres");
            DropTable("dbo.Directors");
            DropTable("dbo.Countries");
        }
    }
}
