namespace WebBanHangDienTu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BaiViet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TieuDe = c.String(nullable: false, maxLength: 150),
                        BiDanh = c.String(maxLength: 150),
                        MoTa = c.String(maxLength: 150),
                        MoTaChiTiet = c.String(),
                        HinhAnh = c.String(maxLength: 250),
                        DanhMucId = c.Int(nullable: false),
                        TieuDeSEO = c.String(maxLength: 250),
                        MoTaSEO = c.String(maxLength: 500),
                        TuKhoaSEO = c.String(maxLength: 250),
                        IsActive = c.Boolean(nullable: false),
                        NguoiTao = c.String(),
                        NgayTao = c.DateTime(nullable: false),
                        NguoiSua = c.String(),
                        NgaySua = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DanhMuc", t => t.DanhMucId, cascadeDelete: true)
                .Index(t => t.DanhMucId);
            
            CreateTable(
                "dbo.DanhMuc",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenDanhMuc = c.String(nullable: false, maxLength: 150),
                        BiDanh = c.String(),
                        MoTa = c.String(),
                        ViTri = c.Int(nullable: false),
                        TieuDeSEO = c.String(maxLength: 150),
                        MoTaSEO = c.String(maxLength: 250),
                        IsActive = c.Boolean(nullable: false),
                        TuKhoaSEO = c.String(),
                        NguoiTao = c.String(),
                        NgayTao = c.DateTime(nullable: false),
                        NguoiSua = c.String(),
                        NgaySua = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BanTin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TieuDe = c.String(nullable: false, maxLength: 150),
                        BiDanh = c.String(),
                        MoTa = c.String(maxLength: 150),
                        MoTaChiTiet = c.String(),
                        HinhAnh = c.String(),
                        DanhMucId = c.Int(nullable: false),
                        TieuDeSEO = c.String(),
                        MoTaSEO = c.String(),
                        TuKhoaSEO = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        NguoiTao = c.String(),
                        NgayTao = c.DateTime(nullable: false),
                        NguoiSua = c.String(),
                        NgaySua = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DanhMuc", t => t.DanhMucId, cascadeDelete: true)
                .Index(t => t.DanhMucId);
            
            CreateTable(
                "dbo.ChiTietDonHang",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DonHangId = c.Int(nullable: false),
                        SanPhamId = c.Int(nullable: false),
                        SoLuong = c.Int(nullable: false),
                        DonGia = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DonHang", t => t.DonHangId, cascadeDelete: true)
                .ForeignKey("dbo.SanPham", t => t.SanPhamId, cascadeDelete: true)
                .Index(t => t.DonHangId)
                .Index(t => t.SanPhamId);
            
            CreateTable(
                "dbo.DonHang",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaCodeDH = c.String(nullable: false),
                        TenKH = c.String(nullable: false),
                        SDT = c.String(nullable: false),
                        DiaChi = c.String(nullable: false),
                        Email = c.String(),
                        TongDH = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SoLuong = c.Int(nullable: false),
                        TypePayment = c.Int(nullable: false),
                        NguoiTao = c.String(),
                        NgayTao = c.DateTime(nullable: false),
                        NguoiSua = c.String(),
                        NgaySua = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SanPham",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenSP = c.String(nullable: false, maxLength: 250),
                        BiDanh = c.String(maxLength: 250),
                        MaCodeSP = c.String(maxLength: 50),
                        MoTa = c.String(maxLength: 150),
                        MoTaChiTiet = c.String(),
                        HinhAnh = c.String(),
                        DonGia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GiaGiam = c.Decimal(precision: 18, scale: 2),
                        SoLuong = c.Int(nullable: false),
                        LuotXem = c.Int(nullable: false),
                        IsHome = c.Boolean(nullable: false),
                        IsSale = c.Boolean(nullable: false),
                        IsFeature = c.Boolean(nullable: false),
                        IsHot = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DanhMucSPId = c.Int(nullable: false),
                        ThuongHieuId = c.Int(),
                        TieuDeSEO = c.String(maxLength: 250),
                        MoTaSEO = c.String(maxLength: 500),
                        TuKhoaSEO = c.String(maxLength: 250),
                        NguoiTao = c.String(),
                        NgayTao = c.DateTime(nullable: false),
                        NguoiSua = c.String(),
                        NgaySua = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DanhMucSP", t => t.DanhMucSPId, cascadeDelete: true)
                .ForeignKey("dbo.ThuongHieu", t => t.ThuongHieuId)
                .Index(t => t.DanhMucSPId)
                .Index(t => t.ThuongHieuId);
            
            CreateTable(
                "dbo.DanhMucSP",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenDanhMuc = c.String(nullable: false, maxLength: 150),
                        BiDanh = c.String(nullable: false, maxLength: 150),
                        MoTa = c.String(),
                        Icon = c.String(),
                        TieuDeSEO = c.String(maxLength: 250),
                        MoTaSEO = c.String(maxLength: 500),
                        TuKhoaSEO = c.String(maxLength: 250),
                        NguoiTao = c.String(),
                        NgayTao = c.DateTime(nullable: false),
                        NguoiSua = c.String(),
                        NgaySua = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HinhAnhSP",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SanPhamId = c.Int(nullable: false),
                        HinhAnh = c.String(),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SanPham", t => t.SanPhamId, cascadeDelete: true)
                .Index(t => t.SanPhamId);
            
            CreateTable(
                "dbo.ThuongHieu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenTH = c.String(nullable: false, maxLength: 150),
                        BiDanh = c.String(maxLength: 150),
                        HinhAnh = c.String(),
                        TieuDeSEO = c.String(maxLength: 250),
                        MoTaSEO = c.String(maxLength: 500),
                        TuKhoaSEO = c.String(maxLength: 250),
                        NguoiTao = c.String(),
                        NgayTao = c.DateTime(nullable: false),
                        NguoiSua = c.String(),
                        NgaySua = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DangKies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        NgayTao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LienHe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ten = c.String(nullable: false, maxLength: 150),
                        Email = c.String(maxLength: 150),
                        WebSite = c.String(),
                        TinNhan = c.String(maxLength: 4000),
                        IsRead = c.Int(nullable: false),
                        NguoiTao = c.String(),
                        NgayTao = c.DateTime(nullable: false),
                        NguoiSua = c.String(),
                        NgaySua = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SystemSetting",
                c => new
                    {
                        SettingKey = c.String(nullable: false, maxLength: 50),
                        SettingValue = c.String(maxLength: 4000),
                        SettingDes = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.SettingKey);
            
            CreateTable(
                "dbo.ThongKe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ThoiGian = c.DateTime(nullable: false),
                        LuotTruyCap = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SanPham", "ThuongHieuId", "dbo.ThuongHieu");
            DropForeignKey("dbo.HinhAnhSP", "SanPhamId", "dbo.SanPham");
            DropForeignKey("dbo.SanPham", "DanhMucSPId", "dbo.DanhMucSP");
            DropForeignKey("dbo.ChiTietDonHang", "SanPhamId", "dbo.SanPham");
            DropForeignKey("dbo.ChiTietDonHang", "DonHangId", "dbo.DonHang");
            DropForeignKey("dbo.BanTin", "DanhMucId", "dbo.DanhMuc");
            DropForeignKey("dbo.BaiViet", "DanhMucId", "dbo.DanhMuc");
            DropIndex("dbo.HinhAnhSP", new[] { "SanPhamId" });
            DropIndex("dbo.SanPham", new[] { "ThuongHieuId" });
            DropIndex("dbo.SanPham", new[] { "DanhMucSPId" });
            DropIndex("dbo.ChiTietDonHang", new[] { "SanPhamId" });
            DropIndex("dbo.ChiTietDonHang", new[] { "DonHangId" });
            DropIndex("dbo.BanTin", new[] { "DanhMucId" });
            DropIndex("dbo.BaiViet", new[] { "DanhMucId" });
            DropTable("dbo.ThongKe");
            DropTable("dbo.SystemSetting");
            DropTable("dbo.LienHe");
            DropTable("dbo.DangKies");
            DropTable("dbo.ThuongHieu");
            DropTable("dbo.HinhAnhSP");
            DropTable("dbo.DanhMucSP");
            DropTable("dbo.SanPham");
            DropTable("dbo.DonHang");
            DropTable("dbo.ChiTietDonHang");
            DropTable("dbo.BanTin");
            DropTable("dbo.DanhMuc");
            DropTable("dbo.BaiViet");
        }
    }
}
