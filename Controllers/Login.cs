using HeThongDatBan.Data;
using HeThongDatBan.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HeThongDatBan.Controllers
{
    public class Login : Controller
    {
        private readonly UsersData _userData;

        public Login(UsersData userData)
        {
            _userData = userData;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string taiKhoan, string matKhau)
        {
            
            User user = await _userData.Login(taiKhoan, matKhau);
            if (user != null)
            {
                string role = "";
                if(user.VaiTro == 1)
                {
                    role = "Admin";
                }
                else if(user.VaiTro == 2)
                {
                    role = "Restaurant";
                }
                else
                {
                    role = "Customer";
                }
                // ✅ Tạo danh sách Claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.TaiKhoan),
                    new Claim(ClaimTypes.Role, role),
                    new Claim("UserId", user.Id.ToString()),
                };

                // ✅ Tạo identity và principal
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                

                // ✅ Đăng nhập bằng Cookie Authentication
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return RedirectToAction("Index", "Home");
            }
            return View();
            
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        // ✅ Hiển thị trang từ chối truy cập
        public IActionResult AccessDenied() => View();
    }
}
