using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
namespace HeThongDatBan.Controllers
{
    public class JwtCookieMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Cookies.ContainsKey("jwt"))
            {
                var token = context.Request.Cookies["jwt"];
                context.Request.Headers.Append("Authorization", "Bearer " + token);
            }

            await _next(context);
        }
    }
}
