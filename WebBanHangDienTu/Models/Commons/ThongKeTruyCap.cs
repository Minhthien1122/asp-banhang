using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebBanHangDienTu.Models.Commons
{
    public class ThongKeTruyCap
    {
        public static string strConnect = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

        public static ThongKeModel ThongKe()
        {
            using (var connect = new SqlConnection(strConnect))
            {
                var item = connect.QueryFirstOrDefault<ThongKeModel>("sp_ThongKeTruyCao", commandType: CommandType.StoredProcedure);
                return item;
            }
        }
    }
}