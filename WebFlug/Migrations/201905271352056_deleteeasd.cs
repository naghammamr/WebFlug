namespace WebFlug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteeasd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "OrderNumber", c => c.String());
            AlterColumn("dbo.Orders", "OrderSatatus", c => c.String());
            AlterColumn("dbo.Orders", "Email", c => c.String());
            DropTable("dbo.OrdersTests");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrdersTests",
                c => new
                    {
                        Order_Id = c.Int(nullable: false, identity: true),
                        OrderNumber = c.String(nullable: false),
                        ProductName = c.String(nullable: false, maxLength: 150),
                        ProductDetails = c.String(nullable: false),
                        ProductPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductQuantity = c.Int(nullable: false),
                        ProductImage = c.String(),
                        ProductLink = c.String(),
                        ProductWeight = c.Single(nullable: false),
                        Deliverfrom = c.String(nullable: false),
                        DeliverTo = c.String(nullable: false),
                        DeliverDate = c.DateTime(nullable: false),
                        AdditionalDetails = c.String(),
                        OrderSatatus = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Order_Id);
            
            AlterColumn("dbo.Orders", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "OrderSatatus", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "OrderNumber", c => c.String(nullable: false));
        }
    }
}
