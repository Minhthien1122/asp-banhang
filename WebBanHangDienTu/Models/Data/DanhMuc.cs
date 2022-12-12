using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebBanHangDienTu.Models.Data;

namespace WebBanHangDienTu.Models.Data
{
    [Table("DanhMuc")]
    public class DanhMuc : Common
    {
        public DanhMuc()
        {
            this.BanTins = new HashSet<BanTin>();
            this.baiViets = new HashSet<BaiViet>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Tên danh mục không được để trống")]
        [StringLength(150)]
        public string TenDanhMuc { get; set; }
        public string BiDanh { get; set; }
        public string MoTa { get; set; }
        public int ViTri { get; set; }
        [StringLength(150)]
        public string TieuDeSEO { get; set; }
        [StringLength(250)]
        public string MoTaSEO { get; set; }
        public bool IsActive { get; set; }
        public string TuKhoaSEO { get; set; }
        public ICollection<BanTin> BanTins { get; set; }
        public ICollection<BaiViet> baiViets { get; set; }

    }
}