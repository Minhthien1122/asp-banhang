using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHangDienTu.Models.Data
{
    [Table("HinhAnhSP")]
    public class HinhAnhSP
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SanPhamId { get; set; }
        public string HinhAnh { get; set; }
        public bool IsDefault { get; set; }
        public virtual SanPham SanPham { get; set; }

    }
}