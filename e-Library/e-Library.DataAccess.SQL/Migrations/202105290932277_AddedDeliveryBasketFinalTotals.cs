namespace e_Library.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDeliveryBasketFinalTotals : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Delivery", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "DeliveryMethod", c => c.String());
            AddColumn("dbo.Orders", "BasketTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "FinalTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "FinalTotal");
            DropColumn("dbo.Orders", "BasketTotal");
            DropColumn("dbo.Orders", "DeliveryMethod");
            DropColumn("dbo.Orders", "Delivery");
        }
    }
}
