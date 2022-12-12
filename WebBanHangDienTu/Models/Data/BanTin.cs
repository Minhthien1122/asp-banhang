using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHangDienTu.Models.Data
{
    [Table("BanTin")]
    public class BanTin : Common
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Bạn không được để trống tiêu đề bản tin")]
        [StringLength(150)]
        public string TieuDe { get; set; }
        public string BiDanh { get; set; }
        [StringLength(150, ErrorMessage = "Không được quá 150 ký tự.")]
        public string MoTa { get; set; }
        [AllowHtml]
        public string MoTaChiTiet { get; set; }
        public string HinhAnh { get; set; }
        public int DanhMucId { get; set; }
        public string TieuDeSEO { get; set; }
        public string MoTaSEO { get; set; }
        public string TuKhoaSEO { get; set; }
        public bool IsActive { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }
    }
}