namespace e_Library.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteOrderStatusClass : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.OrderStatusClasses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderStatusClasses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        OrderStatus = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
