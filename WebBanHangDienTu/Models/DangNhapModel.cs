using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHangDienTu.Models
{
    public class DangNhapModel
    {
        [Required(ErrorMessage = "UserName không để trống.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password không để trống.")]
        public string Password { get; set; }
       
       
    }
}