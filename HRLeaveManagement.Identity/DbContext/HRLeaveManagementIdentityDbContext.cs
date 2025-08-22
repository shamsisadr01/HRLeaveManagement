using HRLeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HRLeaveManagement.Identity.DbContext;

public class HRLeaveManagementIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public HRLeaveManagementIdentityDbContext(DbContextOptions<HRLeaveManagementIdentityDbContext> options) :
        base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(HRLeaveManagementIdentityDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}
public class HRLeaveManagementIdentityDbContextFactory
    : IDesignTimeDbContextFactory<HRLeaveManagementIdentityDbContext>
{
    public HRLeaveManagementIdentityDbContext CreateDbContext(string[] args)
    {
        // مسیر دقیق پروژه Api که شامل appsettings.json است
        var configPath = @"C:\Users\shams\source\repos\HRLeaveManagement\HRLeaveManagement.Api";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(configPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("HRDatabaseContextConnectionString");

        var optionsBuilder = new DbContextOptionsBuilder<HRLeaveManagementIdentityDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new HRLeaveManagementIdentityDbContext(optionsBuilder.Options);
    }
}