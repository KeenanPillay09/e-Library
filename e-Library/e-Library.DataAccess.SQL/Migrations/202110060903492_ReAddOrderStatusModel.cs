namespace e_Library.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReAddOrderStatusModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderStatusModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        OrderStatus = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrderStatusModels");
        }
    }
}
