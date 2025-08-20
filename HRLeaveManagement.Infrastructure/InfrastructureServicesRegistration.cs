using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Loggin;
using HRLeaveManagement.Application.Models.Email;
using HRLeaveManagement.Infrastructure.EmailService;
using HRLeaveManagement.Infrastructure.Loggin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRLeaveManagement.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Fix: Use a lambda to configure EmailSetting  
        services.Configure<EmailSetting>(configuration.GetSection("EmailSetting").Bind);

        services.AddTransient<IEmailSender, EmailSender>();

        services.AddScoped(typeof(IAppLogger<>),typeof(LoggerAdaper<>));
        return services;
    }
}
