namespace e_Library.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletePreBooksTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.PreOrderBooks");
        }
        
        public override void Down()
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
    }
}
