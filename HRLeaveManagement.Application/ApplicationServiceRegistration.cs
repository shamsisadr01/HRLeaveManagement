using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using HRLeaveManagement.Application.MappingProfiles;

namespace HRLeaveManagement.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(options =>
        {
            options.AddProfile<LeaveTypeProfile>();
        });
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}