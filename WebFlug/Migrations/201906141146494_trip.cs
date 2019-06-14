namespace WebFlug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trip : DbMigration
    {
        public override void Up()
        {
            
            AddColumn("dbo.Trips", "AvailableKG", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trips", "AvailableKG");
            
        }
    }
}
