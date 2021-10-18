namespace e_Library.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPreBookTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PreBooks",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 300),
                        Author = c.String(maxLength: 30),
                        Description = c.String(maxLength: 1000),
                        Genre = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Int(nullable: false),
                        Image = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PreBooks");
        }
    }
}
