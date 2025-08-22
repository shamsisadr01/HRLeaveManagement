using HRLeaveManagement.Domain;
using HRLeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.DatabaseContext;

public class HRDatabaseContext : DbContext
{
    public HRDatabaseContext(DbContextOptions<HRDatabaseContext> options) : base(options)
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
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.Now;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}