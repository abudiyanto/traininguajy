namespace TrainingUAJY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Computerv3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        IdTransaction = c.String(nullable: false, maxLength: 128),
                        Created = c.DateTimeOffset(nullable: false, precision: 7),
                        Type = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        TotalTransaction = c.Int(nullable: false),
                        Notes = c.String(),
                        Computer_SKU = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdTransaction)
                .ForeignKey("dbo.Computers", t => t.Computer_SKU)
                .Index(t => t.Computer_SKU);
            
            AddColumn("dbo.Computers", "Stock", c => c.Int(nullable: false));
            AddColumn("dbo.Computers", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "Computer_SKU", "dbo.Computers");
            DropIndex("dbo.Transactions", new[] { "Computer_SKU" });
            DropColumn("dbo.Computers", "Price");
            DropColumn("dbo.Computers", "Stock");
            DropTable("dbo.Transactions");
        }
    }
}
