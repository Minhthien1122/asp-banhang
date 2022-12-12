using System.Collections.Generic;
using System.Linq;
using WebBanHangDienTu.Models.Data;

namespace WebBanHangDienTu.Models
{
    public class ShopingCart
    {
        private AppDbContext dbConect = new AppDbContext();
        public List<ShopingCartItem> items { get; set; }
        public ShopingCart()
        {
            this.items = new List<ShopingCartItem>();
        }
        public string CheckExitItem()
        {
            string errorMsg = "";
            foreach (var item in items)
            {
                var checkExist = dbConect.SanPhams.Where(x=>x.IsActive==true && x.Id == item.SanPhamId).FirstOrDefault();
                if (checkExist == null)
                {
                    errorMsg += "Sản phẩm: "+ item.TenSP + "; ";
                }

            }
            return errorMsg;
        }

        public void ThemVaoGio(ShopingCartItem item, int soluong)
        {
            var CheckExits = items.FirstOrDefault(x => x.SanPhamId == item.SanPhamId);
            if (CheckExits != null)
            {
                CheckExits.SoLuong += soluong;
                CheckExits.TongGia = CheckExits.DonGia * CheckExits.SoLuong;
            }
            else
            {
                items.Add(item);
            }
        }

        public void Xoa(int id)
        {
            var CheckExits = items.SingleOrDefault(x => x.SanPhamId == id);
            if (CheckExits != null)
            {
                items.Remove(CheckExits);
            }

        }
        public void UpdateSL(int id, int soluong)
        {
            var CheckExits = items.SingleOrDefault(x => x.SanPhamId == id);
            if (CheckExits != null)
            {
                CheckExits.SoLuong = soluong;
                CheckExits.TongGia = CheckExits.DonGia * CheckExits.SoLuong;

            }

        }

        public decimal GetTongTien()
        {
            return items.Sum(x => x.TongGia);
        }
        public int GetSoLuong()
        {
            return items.Sum(x => x.SoLuong);
        }
        public void ClearGioHang()
        {
            items.Clear();
        }
    }

    public class ShopingCartItem
    {
        public int SanPhamId { get; set; }
        public string TenSP { get; set; }
        public string TenDanhMucSP { get; set; }
        public string BiDanh { get; set; }
        public string HinhSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal TongGia { get; set; }
    }
}