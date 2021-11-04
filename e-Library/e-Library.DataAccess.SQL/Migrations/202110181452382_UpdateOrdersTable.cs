namespace e_Library.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrdersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DeliveryDate", c => c.DateTime());
            AddColumn("dbo.Orders", "QRCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "QRCode");
            DropColumn("dbo.Orders", "DeliveryDate");
        }
    }
}
