namespace e_Library.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePreOrderModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PreOrderItems", "PreOrderId", "dbo.PreOrders");
            DropIndex("dbo.PreOrderItems", new[] { "PreOrderId" });
            AddColumn("dbo.PreOrders", "BookId", c => c.String());
            AddColumn("dbo.PreOrders", "BookName", c => c.String());
            AlterColumn("dbo.PreOrderItems", "PreOrderId", c => c.String());
            DropColumn("dbo.PreOrders", "FinalTotal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PreOrders", "FinalTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PreOrderItems", "PreOrderId", c => c.String(maxLength: 128));
            DropColumn("dbo.PreOrders", "BookName");
            DropColumn("dbo.PreOrders", "BookId");
            CreateIndex("dbo.PreOrderItems", "PreOrderId");
            AddForeignKey("dbo.PreOrderItems", "PreOrderId", "dbo.PreOrders", "Id");
        }
    }
}
