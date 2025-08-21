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
            options.AddProfile<LeaveAllocationProfile>();
            options.AddProfile<LeaveRequestListProfile>();
        });
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}