using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHangDienTu.Models.Data
{
    [Table("ChiTietDonHang")]
    public class ChiTietDonHang
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DonHangId { get; set; }
        public int SanPhamId { get; set; }
        public int SoLuong { get; set; }
        public Decimal DonGia { get; set; }
        public virtual DonHang DonHang { get; set; }
        public virtual SanPham SanPham { get; set; }


    }
}