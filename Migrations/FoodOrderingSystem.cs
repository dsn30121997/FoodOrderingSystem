
    using System;
    using System.Data.Entity.Migrations;
    
namespace FoodOrderingSystem.Migrations
{
    public partial class FoodOrderingSystem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(nullable: false),
                        Phone = c.Long(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CustomerId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.MenuLists", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.Long(nullable: false),
                        Address = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.MenuLists",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        ItemName = c.String(nullable: false),
                        Category = c.String(nullable: false),
                        Subcategory = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ItemStatus = c.String(),
                        Cart_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ItemId, t.Quantity })
                .ForeignKey("dbo.Carts", t => t.Cart_Id)
                .ForeignKey("dbo.MenuLists", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ItemId)
                .Index(t => t.Cart_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderDate = c.DateTime(nullable: false),
                        OrderStatus = c.String(),
                        SpicialInstruction = c.String(),
                        Cart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Carts", t => t.Cart_Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.Cart_Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.Long(nullable: false),
                        Address = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransactionId = c.Int(nullable: false, identity: true),
                        PaymentMethod = c.String(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.TotalAmount })
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Carts", "ItemId", "dbo.MenuLists");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.OrderItems", "ItemId", "dbo.MenuLists");
            DropForeignKey("dbo.OrderItems", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.Carts", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Payments", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "Cart_Id" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.OrderItems", new[] { "Cart_Id" });
            DropIndex("dbo.OrderItems", new[] { "ItemId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.Carts", new[] { "ItemId" });
            DropIndex("dbo.Carts", new[] { "CustomerId" });
            DropTable("dbo.Payments");
            DropTable("dbo.Employees");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.MenuLists");
            DropTable("dbo.Customers");
            DropTable("dbo.Carts");
            DropTable("dbo.Admins");
        }
    }
}
