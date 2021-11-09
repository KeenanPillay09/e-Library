namespace e_Library.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReturnModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Returns",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        OrderID = c.String(),
                        CustomerName = c.String(),
                        Email = c.String(),
                        Reason = c.String(),
                        RefundType = c.Int(nullable: false),
                        Status = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Returns");
        }
    }
}
