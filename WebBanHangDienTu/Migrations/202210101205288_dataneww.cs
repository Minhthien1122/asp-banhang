namespace WebBanHangDienTu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataneww : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SanPham", "IsHome");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SanPham", "IsHome", c => c.Boolean(nullable: false));
        }
    }
}
