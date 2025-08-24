using System.Security.Claims;
using HRLeaveManagement.Application.Contracts.Identity;
using HRLeaveManagement.Application.Models.Identity;
using HRLeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace HRLeaveManagement.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;

    public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _contextAccessor = contextAccessor;
    }


    public async Task<List<Employee>> GetEmployeesAsync()
    {
        var employees = await _userManager.GetUsersInRoleAsync("Employee");
        return employees.Select(user => new Employee
        {
            Id = int.Parse(user.Id),
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        }).ToList();
    }

    public async Task<Employee> GetEmployeeByIdAsync(string userId)
    {
        var employee = await _userManager.FindByIdAsync(userId);

        return new Employee
        {
            Id = int.Parse(employee.Id),
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email
        };
    }

    public string? UserId => _contextAccessor.HttpContext?.User.FindFirstValue("uid");
}