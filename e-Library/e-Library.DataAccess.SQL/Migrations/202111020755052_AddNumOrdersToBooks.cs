namespace e_Library.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNumOrdersToBooks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "NumOrders", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "NumOrders");
        }
    }
}
