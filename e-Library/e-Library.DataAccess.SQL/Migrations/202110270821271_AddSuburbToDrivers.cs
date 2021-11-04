namespace e_Library.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSuburbToDrivers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drivers", "Suburb", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Drivers", "Suburb");
        }
    }
}
