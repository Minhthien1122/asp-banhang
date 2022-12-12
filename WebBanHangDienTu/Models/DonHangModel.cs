using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHangDienTu.Models
{
    public class DonHangModel
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        public string TenKH { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string SDT { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public int TypePayment { get; set; }

    }
}