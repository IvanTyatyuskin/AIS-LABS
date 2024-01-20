namespace CourseProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Movies", name: "number", newName: "№");
            RenameColumn(table: "dbo.Movies", name: "titleRU", newName: "Название на русском");
            RenameColumn(table: "dbo.Movies", name: "titleEN", newName: "Название на английском");
            RenameColumn(table: "dbo.Movies", name: "year", newName: "Год выпуска");
            RenameColumn(table: "dbo.Movies", name: "duration", newName: "Длительность (мин.)");
            RenameColumn(table: "dbo.Movies", name: "country", newName: "Страна производства");
            RenameColumn(table: "dbo.Movies", name: "genre", newName: "Жанр");
            RenameColumn(table: "dbo.Movies", name: "director", newName: "Режиссер");
            RenameColumn(table: "dbo.Movies", name: "cast", newName: "Актеры");
            RenameColumn(table: "dbo.Movies", name: "rating", newName: "Рейтинг КП");
            RenameColumn(table: "dbo.Movies", name: "votes", newName: "Количество голосов");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Movies", name: "Количество голосов", newName: "votes");
            RenameColumn(table: "dbo.Movies", name: "Рейтинг КП", newName: "rating");
            RenameColumn(table: "dbo.Movies", name: "Актеры", newName: "cast");
            RenameColumn(table: "dbo.Movies", name: "Режиссер", newName: "director");
            RenameColumn(table: "dbo.Movies", name: "Жанр", newName: "genre");
            RenameColumn(table: "dbo.Movies", name: "Страна производства", newName: "country");
            RenameColumn(table: "dbo.Movies", name: "Длительность (мин.)", newName: "duration");
            RenameColumn(table: "dbo.Movies", name: "Год выпуска", newName: "year");
            RenameColumn(table: "dbo.Movies", name: "Название на английском", newName: "titleEN");
            RenameColumn(table: "dbo.Movies", name: "Название на русском", newName: "titleRU");
            RenameColumn(table: "dbo.Movies", name: "№", newName: "number");
        }
    }
}
