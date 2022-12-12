namespace WebBanHangDienTu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SanPhamGioHang",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SanPhamId = c.Int(),
                        GioHangId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GioHang", t => t.GioHangId)
                .ForeignKey("dbo.SanPham", t => t.SanPhamId)
                .Index(t => t.SanPhamId)
                .Index(t => t.GioHangId);
            
            CreateTable(
                "dbo.GioHang",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SanPhamGioHang", "SanPhamId", "dbo.SanPham");
            DropForeignKey("dbo.SanPhamGioHang", "GioHangId", "dbo.GioHang");
            DropIndex("dbo.SanPhamGioHang", new[] { "GioHangId" });
            DropIndex("dbo.SanPhamGioHang", new[] { "SanPhamId" });
            DropTable("dbo.GioHang");
            DropTable("dbo.SanPhamGioHang");
        }
    }
}
