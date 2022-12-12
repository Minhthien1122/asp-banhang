using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangDienTu.Models
{
    public abstract class Common
    {
        public string NguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
        public string NguoiSua { get; set; }
        public DateTime NgaySua { get; set; }
    }
}