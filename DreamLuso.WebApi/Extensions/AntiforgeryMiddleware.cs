using Microsoft.AspNetCore.Antiforgery;

namespace DreamLuso.WebApi.Extensions;

public class AntiforgeryMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IAntiforgery _antiforgery;

    public AntiforgeryMiddleware(RequestDelegate next, IAntiforgery antiforgery)
    {
        _next = next;
        _antiforgery = antiforgery;
    }

    public Task Invoke(HttpContext context)
    {
        var tokens = _antiforgery.GetAndStoreTokens(context);
        context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
            new CookieOptions { HttpOnly = false, Secure = true, SameSite = SameSiteMode.Strict });

        return _next(context);
    }
}
