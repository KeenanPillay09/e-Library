namespace e_Library.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSalesToBooks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "NumSales", c => c.Int(nullable: false));
            AddColumn("dbo.Books", "TotalSales", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "TotalSales");
            DropColumn("dbo.Books", "NumSales");
        }
    }
}
