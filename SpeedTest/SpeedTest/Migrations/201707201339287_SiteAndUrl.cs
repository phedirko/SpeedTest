namespace SpeedTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SiteAndUrl : DbMigration
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
                        ElapsedTime = c.Time(nullable: false, precision: 7),
                        Measurement_Id = c.Int(),
                        Url_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Measurements", t => t.Measurement_Id)
                .ForeignKey("dbo.Urls", t => t.Url_Id)
                .Index(t => t.Measurement_Id)
                .Index(t => t.Url_Id);
            
            CreateTable(
                "dbo.Urls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Location = c.String(),
                        Elapsed = c.Time(nullable: false, precision: 7),
                        Site_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.Site_Id)
                .Index(t => t.Site_Id);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeasuredUrls", "Url_Id", "dbo.Urls");
            DropForeignKey("dbo.Urls", "Site_Id", "dbo.Sites");
            DropForeignKey("dbo.Measurements", "Site_Id", "dbo.Sites");
            DropForeignKey("dbo.MeasuredUrls", "Measurement_Id", "dbo.Measurements");
            DropIndex("dbo.Urls", new[] { "Site_Id" });
            DropIndex("dbo.MeasuredUrls", new[] { "Url_Id" });
            DropIndex("dbo.MeasuredUrls", new[] { "Measurement_Id" });
            DropIndex("dbo.Measurements", new[] { "Site_Id" });
            DropTable("dbo.Sites");
            DropTable("dbo.Urls");
            DropTable("dbo.MeasuredUrls");
            DropTable("dbo.Measurements");
        }
    }
}
