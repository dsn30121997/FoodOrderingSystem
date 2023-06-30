using System;
using System.Data.Entity.Migrations;

public partial class Added : DbMigration
{
    public override void Up()
    {
        AddColumn("dbo.Carts", "SerialId", c => c.Int(nullable: false));
    }
    
    public override void Down()
    {
        DropColumn("dbo.Carts", "SerialId");
    }
}
