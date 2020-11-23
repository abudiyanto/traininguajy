namespace TrainingUAJY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComputerV1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        IdBrand = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.IdBrand);
            
            CreateTable(
                "dbo.Computers",
                c => new
                    {
                        SKU = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Description = c.String(),
                        Brand_IdBrand = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SKU)
                .ForeignKey("dbo.Brands", t => t.Brand_IdBrand)
                .Index(t => t.Brand_IdBrand);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Computers", "Brand_IdBrand", "dbo.Brands");
            DropIndex("dbo.Computers", new[] { "Brand_IdBrand" });
            DropTable("dbo.Computers");
            DropTable("dbo.Brands");
        }
    }
}
