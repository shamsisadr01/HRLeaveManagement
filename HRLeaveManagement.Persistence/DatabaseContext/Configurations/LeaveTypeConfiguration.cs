using HRLeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRLeaveManagement.Persistence.DatabaseContext.Configurations;

public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.ToTable("LeaveTypes");

        // q: HasData Write
        builder.HasData(
            new LeaveType
            {
                Id = 1,
                Name = "Annual Leave",
                DefaultDays = 20,
                CreatedAt = new DateTime(2025, 01, 01),
                UpdatedAt = new DateTime(2025, 01, 01)
            },
            new LeaveType
            {
                Id = 2,
                Name = "Sick Leave",
                DefaultDays = 10,
                CreatedAt = new DateTime(2025, 01, 01),
                UpdatedAt = new DateTime(2025, 01, 01)
            },
            new LeaveType
            {
                Id = 3,
                Name = "Maternity Leave",
                DefaultDays = 90,
                CreatedAt = new DateTime(2025, 01, 01),
                UpdatedAt = new DateTime(2025, 01, 01)
            }
        );


        // q: Configure properties
        builder.Property(lt => lt.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}