namespace e_Library.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeSuburbNullOrders : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Suburb", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Suburb", c => c.String(nullable: false));
        }
    }
}
