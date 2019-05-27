namespace WebFlug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class frst : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "OrderNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "OrderSatatus", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Email", c => c.String(nullable: false));
            
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Email", c => c.String());
            AlterColumn("dbo.Orders", "OrderSatatus", c => c.String());
            AlterColumn("dbo.Orders", "OrderNumber", c => c.String());
            
        }
    }
}
