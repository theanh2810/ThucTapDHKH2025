namespace HeThongDatBan.Middleware
{
    public class RedirectUnauthorizedMiddleware
    {
        private readonly RequestDelegate _next;

        public RedirectUnauthorizedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 401 && !context.Request.Path.StartsWithSegments("/Login/Index"))
            {
                context.Response.Redirect("/Login/Index");
            }
        }
    }

}
