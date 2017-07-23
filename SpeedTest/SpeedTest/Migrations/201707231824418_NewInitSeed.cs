namespace SpeedTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewInitSeed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Measurements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateOfMeasuring = c.DateTime(nullable: false),
                        Site_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.Site_Id)
                .Index(t => t.Site_Id);
            
            CreateTable(
                "dbo.MeasuredUrls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        ElapsedTime = c.Time(nullable: false, precision: 7),
                        Measurement_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Measurements", t => t.Measurement_Id)
                .Index(t => t.Measurement_Id);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Urls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Location = c.String(),
                        Site_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.Site_Id)
                .Index(t => t.Site_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Urls", "Site_Id", "dbo.Sites");
            DropForeignKey("dbo.Measurements", "Site_Id", "dbo.Sites");
            DropForeignKey("dbo.MeasuredUrls", "Measurement_Id", "dbo.Measurements");
            DropIndex("dbo.Urls", new[] { "Site_Id" });
            DropIndex("dbo.MeasuredUrls", new[] { "Measurement_Id" });
            DropIndex("dbo.Measurements", new[] { "Site_Id" });
            DropTable("dbo.Urls");
            DropTable("dbo.Sites");
            DropTable("dbo.MeasuredUrls");
            DropTable("dbo.Measurements");
        }
    }
}
