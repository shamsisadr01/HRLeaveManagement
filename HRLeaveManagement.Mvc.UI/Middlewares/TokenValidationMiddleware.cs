using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace HRLeaveManagement.Mvc.UI.Middlewares;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JwtSecurityTokenHandler _jwtHandler = new JwtSecurityTokenHandler();

    public TokenValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Cookies.ContainsKey("AuthToken"))
        {
            var token = context.Request.Cookies["AuthToken"];
            var jwtToken = _jwtHandler.ReadJwtToken(token);

            if (jwtToken.ValidTo < DateTime.Now)
            {
                // توکن منقضی شده، کاربر Logout شود
                await context.SignOutAsync();
                context.Response.Redirect("/Account/Login");
                return;
            }
        }

        await _next(context);
    }
}