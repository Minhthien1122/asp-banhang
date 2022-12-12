using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHangDienTu.Models.Data
{
    [Table("DanhMucSP")]
    public class DanhMucSP : Common
    {
        public DanhMucSP()
        {
            this.SanPhams = new HashSet<SanPham>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string TenDanhMuc { get; set; }
        [Required]
        [StringLength(150)]
        public string BiDanh { get; set; }
        public string MoTa { get; set; }
        public string Icon { get; set; }
        [StringLength(250)]
        public string TieuDeSEO { get; set; }
        [StringLength(500)]
        public string MoTaSEO { get; set; }
        [StringLength(250)]
        public string TuKhoaSEO { get; set; }

        public ICollection<SanPham> SanPhams { get; set; }

    }
}