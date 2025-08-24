using HRLeaveManagement.Application.Contracts.Identity;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.DatabaseContext;

public class HRDatabaseContext : DbContext
{
    private readonly IUserService _userService;
    public HRDatabaseContext(DbContextOptions<HRDatabaseContext> options,IUserService userService) : base(options)
    {
    }

    public DbSet<LeaveType> LeaveTypes { get; set; }

    public DbSet<LeaveRequest> LeaveRequests { get; set; }

    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRDatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            entry.Entity.UpdatedAt = DateTime.Now;
            entry.Entity.UpdateBy = _userService.UserId;
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.Now;
                entry.Entity.CreateBy = _userService.UserId;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}