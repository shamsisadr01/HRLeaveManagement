using HRLeaveManagement.Mvc.UI.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IAuthenticationService = HRLeaveManagement.Mvc.UI.Contracts.IAuthenticationService;

namespace HRLeaveManagement.Mvc.UI.Services;

public class AuthenticationService : BaseHttpService,IAuthenticationService
{
    private readonly HttpContext _httpContext;
    private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;
    public AuthenticationService(IClient client,IHttpContextAccessor accessor, JwtSecurityTokenHandler jwtSecurityTokenHandler) : 
        base(client)
    {
        this.jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        _httpContext = accessor.HttpContext;
    }

    public async Task<bool> AuthenticateAsync(string email, string password)
    {
        try
        {
            AuthRequest authenticationRequest = new AuthRequest() { Email = email, Password = password };
            var authenticationResponse = await _client.LoginAsync(authenticationRequest);
            if (authenticationResponse.Token != string.Empty)
            {
                var claims = GetClaims(authenticationResponse.Token);

                var identity = new ClaimsIdentity(claims);
                var principal = new ClaimsPrincipal(identity);

                await _httpContext.SignInAsync(principal);

                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> RegisterAsync(string firstName, string lastName, string userName, string email, string password)
    {
        var registrationRequest = new RegisterRequest() { FirstName = firstName, LastName = lastName, Email = email, Username = userName, Password = password };
        var response = await _client.RegisterAsync(registrationRequest);

        if (!string.IsNullOrEmpty(response.UserId))
        {
            return true;
        }
        return false;
    }

    public async Task Logout()
    {
        await _httpContext.SignOutAsync();
    }

    private  List<Claim> GetClaims(string token)
    {
        var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(token);
        var claims = tokenContent.Claims.ToList();
        claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
        return claims;
    }

}