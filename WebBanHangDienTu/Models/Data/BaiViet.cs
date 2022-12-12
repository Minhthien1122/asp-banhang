using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHangDienTu.Models.Data
{
    [Table("BaiViet")]
    public class BaiViet : Common
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string TieuDe { get; set; }
        [StringLength(150)]
        public string BiDanh { get; set; }
        [StringLength(150, ErrorMessage = "Không được quá 150 ký tự.")]
        public string MoTa { get; set; }
        [AllowHtml]
        public string MoTaChiTiet { get; set; }
        [StringLength(250)]
        public string HinhAnh { get; set; }
        public int DanhMucId { get; set; }
        [StringLength(250)]
        public string TieuDeSEO { get; set; }
        [StringLength(500)]
        public string MoTaSEO { get; set; }
        [StringLength(250)]
        public string TuKhoaSEO { get; set; }
        public bool IsActive { get; set; }
        public virtual DanhMuc DanhMuc { get; set; }
    }
}