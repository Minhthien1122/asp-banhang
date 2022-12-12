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
    [Table("ThuongHieu")]
    public class ThuongHieu : Common
    {
        public ThuongHieu()
        {
            this.SanPhams = new HashSet<SanPham>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string TenTH { get; set; }
        [StringLength(150)]
        public string BiDanh { get; set; }
        public string HinhAnh { get; set; }

        [StringLength(250)]
        public string TieuDeSEO { get; set; }
        [StringLength(500)]
        public string MoTaSEO { get; set; }
        [StringLength(250)]
        public string TuKhoaSEO { get; set; }
        public virtual ICollection<SanPham> SanPhams { get; set; }

    }
}