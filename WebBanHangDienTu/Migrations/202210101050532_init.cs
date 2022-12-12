namespace WebBanHangDienTu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SanPham", "ThuongHieuId", "dbo.ThuongHieu");
            DropIndex("dbo.SanPham", new[] { "ThuongHieuId" });
            AlterColumn("dbo.SanPham", "ThuongHieuId", c => c.Int(nullable: false));
            CreateIndex("dbo.SanPham", "ThuongHieuId");
            AddForeignKey("dbo.SanPham", "ThuongHieuId", "dbo.ThuongHieu", "Id", cascadeDelete: true);
            DropTable("dbo.SystemSetting");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SystemSetting",
                c => new
                    {
                        SettingKey = c.String(nullable: false, maxLength: 50),
                        SettingValue = c.String(maxLength: 4000),
                        SettingDes = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.SettingKey);
            
            DropForeignKey("dbo.SanPham", "ThuongHieuId", "dbo.ThuongHieu");
            DropIndex("dbo.SanPham", new[] { "ThuongHieuId" });
            AlterColumn("dbo.SanPham", "ThuongHieuId", c => c.Int());
            CreateIndex("dbo.SanPham", "ThuongHieuId");
            AddForeignKey("dbo.SanPham", "ThuongHieuId", "dbo.ThuongHieu", "Id");
        }
    }
}
