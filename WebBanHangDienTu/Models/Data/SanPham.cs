using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangDienTu.Models.Data;

namespace WebBanHangDienTu.Models.Data
{
    [Table("SanPham")]
    public class SanPham : Common
    {
        public SanPham()
        {
            this.HinhAnhSP = new HashSet<HinhAnhSP>();
            this.ChiTietDonHangs = new HashSet<ChiTietDonHang>();

        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string TenSP { get; set; }
        [StringLength(250)]
        public string BiDanh { get; set; }
        [StringLength(50)]
        public string MaCodeSP { get; set; }
        [StringLength(150, ErrorMessage = "Không được quá 150 ký tự.")]
        public string MoTa { get; set; }
        [AllowHtml]
        public string MoTaChiTiet { get; set; }
        public string HinhAnh { get; set; }
        public Decimal GiaNhap { get; set; }
        public Decimal DonGia { get; set; }
        public Decimal? GiaGiam { get; set; }
        public int SoLuong { get; set; }
        public int LuotXem { get; set; }
        public bool IsSale { get; set; }
        public bool IsFeature { get; set; }
        public bool IsHot { get; set; }
        public bool IsActive { get; set; }
        public int DanhMucSPId { get; set; }
        public int ThuongHieuId { get; set; }
        [StringLength(250)]
        public string TieuDeSEO { get; set; }
        [StringLength(500)]
        public string MoTaSEO { get; set; }
        [StringLength(250)]
        public string TuKhoaSEO { get; set; }

        public virtual DanhMucSP DanhMucSP { get; set; }
        public virtual ThuongHieu ThuongHieu { get; set; }
        public virtual ICollection<HinhAnhSP> HinhAnhSP { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

    }
}