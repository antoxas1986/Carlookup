namespace CarLookUp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BodyType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeOfBody = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Car",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BodyTypeId = c.Int(nullable: false),
                        Maker = c.String(),
                        Model = c.String(),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BodyType", t => t.BodyTypeId, cascadeDelete: true)
                .Index(t => t.BodyTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Car", "BodyTypeId", "dbo.BodyType");
            DropIndex("dbo.Car", new[] { "BodyTypeId" });
            DropTable("dbo.Car");
            DropTable("dbo.BodyType");
        }
    }
}
