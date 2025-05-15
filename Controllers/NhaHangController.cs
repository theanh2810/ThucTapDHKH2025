using Microsoft.AspNetCore.Mvc;

namespace HeThongDatBan.Controllers
{
    public class NhaHangController : Controller
    {
        public IActionResult Index()
        {
            var danhSachNhaHang = new List<NhaHang>
            {
                new NhaHang { Id = 1, TenNhaHang = "Nhà hàng ABC", DiaChi = "123 Đường A, TP.HCM", SDT = "0123 456 789", ImageUrl = "/images/nhahang1.jpg" },
                new NhaHang { Id = 2, TenNhaHang = "Nhà hàng XYZ", DiaChi = "456 Đường B, Hà Nội", SDT = "0987 654 321", ImageUrl = "/images/nhahang2.jpg" }
            };
            return View(danhSachNhaHang);
        }
    }

    public class NhaHang
    {
        public int Id { get; set; }
        public string? TenNhaHang { get; set; }
        public string? DiaChi { get; set; }
        public string? SDT { get; set; }
        public string? ImageUrl { get; set; }
    }
}
