using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHangDienTu.Models.Data
{
    [Table("SanPhamGioHang")]
    public class SanPhamGioHang
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? SanPhamId { get; set; }
        public int? GioHangId { get; set; }
        public virtual SanPham SanPham { get; set; }
        public virtual GioHang GioHang { get; set; }

    }
}