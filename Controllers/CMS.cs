using HeThongDatBan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HeThongDatBan.Controllers
{
    //[RedirectIfUnauthorized]
    //[Authorize]

    public class CMS : Controller
    {
        //[Authorize(Roles = "Admin,Restaurant")]
        [Authorize]
        public IActionResult Index()
        {
            //return View();
            if (User.Identity.IsAuthenticated)
            {
                Console.WriteLine("user đã đăng nhập: " + User.Identity.Name);
                return View();
            }
            else
            {
                Console.WriteLine("user chưa đăng nhập");
                return RedirectToAction("index", "login");
            }
        }
    }
}
