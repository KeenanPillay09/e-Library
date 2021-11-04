namespace e_Library.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAreaToOrders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Suburb", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "Area", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Area");
            DropColumn("dbo.Orders", "Suburb");
        }
    }
}
