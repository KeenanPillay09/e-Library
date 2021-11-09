namespace e_Library.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPreOrders : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PreBooks", newName: "PreOrderBooks");
            CreateTable(
                "dbo.PreOrderItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PreOrderId = c.String(maxLength: 128),
                        BookId = c.String(),
                        BookName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.String(),
                        Quantity = c.Int(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PreOrders", t => t.PreOrderId)
                .Index(t => t.PreOrderId);
            
            CreateTable(
                "dbo.PreOrders",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        City = c.String(nullable: false),
                        ZipCode = c.String(nullable: false),
                        OrderStatus = c.String(),
                        Delivery = c.Int(nullable: false),
                        DeliveryMethod = c.String(),
                        BasketTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinalTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Driver = c.String(),
                        Suburb = c.String(),
                        Area = c.Int(nullable: false),
                        DeliveryDate = c.DateTime(),
                        QRCode = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PreOrderStatusModels",
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
            DropForeignKey("dbo.PreOrderItems", "PreOrderId", "dbo.PreOrders");
            DropIndex("dbo.PreOrderItems", new[] { "PreOrderId" });
            DropTable("dbo.PreOrderStatusModels");
            DropTable("dbo.PreOrders");
            DropTable("dbo.PreOrderItems");
            RenameTable(name: "dbo.PreOrderBooks", newName: "PreBooks");
        }
    }
}
