namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingGenreData : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into dbo.Genres values('Action')");
            Sql("Insert into dbo.Genres values('Comedy')");
            Sql("Insert into dbo.Genres values('Romance')");
            Sql("Insert into dbo.Genres values('Thriller')");
            Sql("Insert into dbo.Genres values('Family')");
        }
        
        public override void Down()
        {
        }
    }
}
