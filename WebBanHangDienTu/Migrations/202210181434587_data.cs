namespace WebBanHangDienTu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class data : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SanPhamGioHang", "GioHangId", "dbo.GioHang");
            DropForeignKey("dbo.SanPhamGioHang", "SanPhamId", "dbo.SanPham");
            DropIndex("dbo.SanPhamGioHang", new[] { "SanPhamId" });
            DropIndex("dbo.SanPhamGioHang", new[] { "GioHangId" });
            DropTable("dbo.SanPhamGioHang");
            DropTable("dbo.GioHang");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GioHang",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SanPhamGioHang",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SanPhamId = c.Int(),
                        GioHangId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.SanPhamGioHang", "GioHangId");
            CreateIndex("dbo.SanPhamGioHang", "SanPhamId");
            AddForeignKey("dbo.SanPhamGioHang", "SanPhamId", "dbo.SanPham", "Id");
            AddForeignKey("dbo.SanPhamGioHang", "GioHangId", "dbo.GioHang", "Id");
        }
    }
}
