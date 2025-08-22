using HRLeaveManagement.Application.Models.Identity;

namespace HRLeaveManagement.Application.Contracts.Identity;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(AuthRequest request);
    Task<RegisterResponset> RegisterAsync(RegisterRequest request);
}