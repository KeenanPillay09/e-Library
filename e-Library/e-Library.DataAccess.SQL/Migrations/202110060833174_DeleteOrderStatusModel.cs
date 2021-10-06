namespace e_Library.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteOrderStatusModel : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.OrderStatusModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderStatusModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        OrderStatus = c.String(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
