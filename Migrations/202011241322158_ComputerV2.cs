namespace TrainingUAJY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComputerV2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Processors",
                c => new
                    {
                        IdProcessor = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Year = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.IdProcessor);
            
            AddColumn("dbo.Computers", "ProductionYear", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Computers", "ScreenSize", c => c.Int(nullable: false));
            AddColumn("dbo.Computers", "RAM", c => c.Int(nullable: false));
            AddColumn("dbo.Computers", "Storage", c => c.Int(nullable: false));
            AddColumn("dbo.Computers", "Processor_IdProcessor", c => c.String(maxLength: 128));
            CreateIndex("dbo.Computers", "Processor_IdProcessor");
            AddForeignKey("dbo.Computers", "Processor_IdProcessor", "dbo.Processors", "IdProcessor");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Computers", "Processor_IdProcessor", "dbo.Processors");
            DropIndex("dbo.Computers", new[] { "Processor_IdProcessor" });
            DropColumn("dbo.Computers", "Processor_IdProcessor");
            DropColumn("dbo.Computers", "Storage");
            DropColumn("dbo.Computers", "RAM");
            DropColumn("dbo.Computers", "ScreenSize");
            DropColumn("dbo.Computers", "ProductionYear");
            DropTable("dbo.Processors");
        }
    }
}
