namespace e_Library.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPreOrders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PreOrderBooks",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        BookName = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                        Category = c.String(),
                        Author = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PreOrderBooks");
        }
    }
}
