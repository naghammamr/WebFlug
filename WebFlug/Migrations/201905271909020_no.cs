namespace WebFlug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class no : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Offers", "OfferSatatus", c => c.String(maxLength: 20));
            CreateIndex("dbo.Trips", "UserId");
            AddForeignKey("dbo.Trips", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Trips", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trips", "Email", c => c.String());
            DropForeignKey("dbo.Trips", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Trips", new[] { "UserId" });
            AlterColumn("dbo.Offers", "OfferSatatus", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.Trips", "UserId");
        }
    }
}
