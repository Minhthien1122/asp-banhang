using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebBanHangDienTu.Models.Data;

namespace WebBanHangDienTu.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("ConnectionString") {  }
        public DbSet<ThuongHieu> thuongHieus { get; set; }
        public DbSet<DangKies> DangKies { get; set; }
        public DbSet<ThongKe> ThongKes { get; set; }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<BanTin> BanTins { get; set; }
        public DbSet<BaiViet> BaiViets { get; set; }
        public DbSet<DanhMucSP> DanhMucSPs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<HinhAnhSP> HinhAnhSPs { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DbSet<LienHe> LienHes { get; set; }
    }
}