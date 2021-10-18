namespace e_Library.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePreBooksDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PreBooks", "ReleaseDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PreBooks", "ReleaseDate", c => c.DateTime(nullable: false));
        }
    }
}
