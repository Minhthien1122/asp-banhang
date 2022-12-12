using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHangDienTu.Models
{
    public class ThongkeSPModel
    {
        public DateTime ngayTao { get; set; }
        public Decimal TongDh { get; set; }
    }
}