using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HeThongDatBan.Models;

namespace HeThongDatBan.Controllers;
//[Route("CMS/Admin/[action]")]
public class AdminController : Controller
{
    public IActionResult QuanLyNhaHang()
    {
        return View("~/Views/CMS/Admin/QuanLyNhaHang.cshtml");
    }
    public IActionResult QuanLyNguoiDung()
    {
        return View("~/Views/CMS/Admin/QuanLyNguoiDung.cshtml");
    }

}
