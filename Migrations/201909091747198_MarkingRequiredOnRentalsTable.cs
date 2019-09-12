namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MarkingRequiredOnRentalsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rentals", "Customers_Id", "dbo.Customers");
            DropForeignKey("dbo.Rentals", "Movies_Id", "dbo.Movies");
            DropIndex("dbo.Rentals", new[] { "Customers_Id" });
            DropIndex("dbo.Rentals", new[] { "Movies_Id" });
            AlterColumn("dbo.Rentals", "Customers_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Rentals", "Movies_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Rentals", "Customers_Id");
            CreateIndex("dbo.Rentals", "Movies_Id");
            AddForeignKey("dbo.Rentals", "Customers_Id", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Rentals", "Movies_Id", "dbo.Movies", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "Movies_Id", "dbo.Movies");
            DropForeignKey("dbo.Rentals", "Customers_Id", "dbo.Customers");
            DropIndex("dbo.Rentals", new[] { "Movies_Id" });
            DropIndex("dbo.Rentals", new[] { "Customers_Id" });
            AlterColumn("dbo.Rentals", "Movies_Id", c => c.Int());
            AlterColumn("dbo.Rentals", "Customers_Id", c => c.Int());
            CreateIndex("dbo.Rentals", "Movies_Id");
            CreateIndex("dbo.Rentals", "Customers_Id");
            AddForeignKey("dbo.Rentals", "Movies_Id", "dbo.Movies", "Id");
            AddForeignKey("dbo.Rentals", "Customers_Id", "dbo.Customers", "Id");
        }
    }
}
