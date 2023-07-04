using System;
using System.Data.Entity.Migrations;

public partial class updateDateinPayment : DbMigration
{
    public override void Up()
    {
        AlterColumn("dbo.Payments", "PaymentDate", c => c.DateTime(nullable: false));
    }
    
    public override void Down()
    {
        AlterColumn("dbo.Payments", "PaymentDate", c => c.DateTime(nullable: false));
    }
}
