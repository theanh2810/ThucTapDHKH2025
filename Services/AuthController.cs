using HeThongDatBan.Data;
using HeThongDatBan.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HeThongDatBan.Services
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UsersData _user;
        private readonly IConfiguration _config;
        public AuthController(UsersData userController, IConfiguration config)
        {
            _user = userController;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Giả lập kiểm tra tài khoản (Bạn có thể thay bằng DB)
            var user = await _user.Login(request.Username, request.Password);
            if (user == null) return Unauthorized(new { message = "Sai tài khoản hoặc mật khẩu!" });

            // Tạo Token
            var token = GenerateJwtToken(user);

            // Lưu Token vào Cookie
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,       // Ngăn JavaScript truy cập (chống XSS)
                Secure = false,         // Chỉ gửi qua HTTPS (bắt buộc khi chạy production)
                SameSite = SameSiteMode.Strict, // Ngăn CSRF
                Expires = DateTime.UtcNow.AddMinutes(30) // Hết hạn sau 30 phút
            };
            Response.Cookies.Append("accessToken", token, cookieOptions);

            return Ok(new { Token = token });
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("accessToken"); // Xóa token trong Cookie
            return Ok(new { message = "Đăng xuất thành công" });
        }

        //[HttpPost]
        //public async Task<ApiResult<TokenDetails>> Login([FromBody] LoginRequest request)
        //{
        //    try
        //    {
        //        var user = FakeUserStore.GetUser(request.Username, request.Password);
        //        if (user == null) return new ApiResult<TokenDetails>(false, "Dang nhap khong thanh cong");

        //        var token = GenerateJwtToken(user);
        //        TokenDetails details = new TokenDetails { Token = token, Role = user.Role };

        //        return new ApiResult<TokenDetails>(true, "Dang nhap thanh cong", details, 1);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiResult<TokenDetails>(false, ex.Message);
        //    }
        //}

        private string GenerateJwtToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]);
            var tokenHandler = new JwtSecurityTokenHandler();

            string vaitro = "";
            if(user.VaiTro == 1)
            {
                vaitro = "Admin";
            }
            else if(user.VaiTro == 2)
            {
                vaitro = "Restaurant";
            }
            else
            {
                vaitro = "Customer";
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.TaiKhoan), // Định danh User
                new Claim(ClaimTypes.Role, vaitro), // Role chuẩn của ASP.NET Core
                new Claim(ClaimTypes.Name, user.TaiKhoan)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(_config["JwtSettings:ExpirationInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Issuer = _config["JwtSettings:Issuer"],
                Audience = _config["JwtSettings:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
    // Model nhận dữ liệu từ client
    public class LoginRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
    // Giả lập danh sách user trong hệ thống
    public static class FakeUserStore
    {
        private static List<User1> Users = new()
    {
        new User1 { Username = "admin", Password = "123", Role = "Admin" },
        new User1 { Username = "restaurant1", Password = "123", Role = "Restaurant" },
        new User1 { Username = "customer1", Password = "123", Role = "Customer" }
    };

        public static User1 GetUser(string username, string password)
            => Users.FirstOrDefault(u => u.Username == username && u.Password == password);
    }
    public class User1
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }

    public class TokenDetails
    {
        public string? Token { get; set; }
        public string? Role { get; set; }
    }
}
