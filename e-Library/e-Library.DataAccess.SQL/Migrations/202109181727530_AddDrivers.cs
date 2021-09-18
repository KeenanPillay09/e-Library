namespace e_Library.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDrivers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Driver", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Driver");
        }
    }
}
