using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHangDienTu.Models
{
    public class DangKyModel
    {
        [Required(ErrorMessage = "UserName không để trống.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password không để trống.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword không để trống.")]
        [Compare("Password", ErrorMessage = "Password Không giống nhau.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Email không để trống.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public static implicit operator string(DangKyModel v)
        {
            throw new NotImplementedException();
        }
    }
}