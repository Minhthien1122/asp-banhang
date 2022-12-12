using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHangDienTu.Models.Data
{
    [Table("GioHang")]
    public class GioHang
    {
        public GioHang()
        {
            this.SanPhamGioHangs = new HashSet<SanPhamGioHang>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual ICollection<SanPhamGioHang> SanPhamGioHangs { get; set; }


    }
}