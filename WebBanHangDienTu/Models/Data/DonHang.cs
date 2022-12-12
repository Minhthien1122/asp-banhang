using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebBanHangDienTu.Models.Data;

namespace WebBanHangDienTu.Models.Data
{
    [Table("DonHang")]
    public class DonHang : Common
    {
        public DonHang()
        {
            this.ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string MaCodeDH { get; set; }
        [Required(ErrorMessage ="Tên không được để trống")]
        public string TenKH { get; set; }
        [Required(ErrorMessage ="Số điện thoại không được để trống")]
        public string SDT { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public Decimal TongDH { get; set; }
        public int SoLuong { get; set; }
        public int TypePayment { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }


    }
}