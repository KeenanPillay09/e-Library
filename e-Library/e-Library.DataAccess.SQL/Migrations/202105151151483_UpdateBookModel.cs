namespace e_Library.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBookModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Name", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Name", c => c.String(maxLength: 30));
        }
    }
}
