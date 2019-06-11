namespace WebFlug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            
            AlterColumn("dbo.Orders", "ProductWeight", c => c.Int(nullable: false));
            
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "ProductWeight", c => c.Single(nullable: false));
            
        }
    }
}
