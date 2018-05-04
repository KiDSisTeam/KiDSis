namespace KiDSisMvcWebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropColumnBooksBooksCategori_Id : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "BooksCategory_Id", "dbo.BooksCategories");
            DropIndex("dbo.Books", new[] { "BooksCategory_Id" });
            RenameColumn(table: "dbo.Books", name: "BooksCategory_Id", newName: "BooksCategoryId");
            AlterColumn("dbo.Books", "BooksCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "BooksCategoryId");
            AddForeignKey("dbo.Books", "BooksCategoryId", "dbo.BooksCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "BooksCategoryId", "dbo.BooksCategories");
            DropIndex("dbo.Books", new[] { "BooksCategoryId" });
            AlterColumn("dbo.Books", "BooksCategoryId", c => c.Int());
            RenameColumn(table: "dbo.Books", name: "BooksCategoryId", newName: "BooksCategory_Id");
            CreateIndex("dbo.Books", "BooksCategory_Id");
            AddForeignKey("dbo.Books", "BooksCategory_Id", "dbo.BooksCategories", "Id");
        }
    }
}
