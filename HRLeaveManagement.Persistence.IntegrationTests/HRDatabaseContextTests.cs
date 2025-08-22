using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HRLeaveManagement.Persistence.IntegrationTests
{
    public class HRDatabaseContextTests
    {
        private readonly HRDatabaseContext _context;
        public HRDatabaseContextTests()
        {
            var options = new DbContextOptionsBuilder<HRDatabaseContext>()
                .UseInMemoryDatabase(databaseName: "HRLeaveManagement")
                .Options;
            _context = new HRDatabaseContext(options);
        }

        [Fact]
        public async void Save_SetDateCreatedValue()
        {
            var leaveType = new LeaveType
            {
                Id = 2,
                Name = "Annual Leave",
                DefaultDays = 20,
            };

            await _context.LeaveTypes.AddAsync(leaveType);
            await _context.SaveChangesAsync();

            leaveType.CreatedAt.ShouldNotBeNull();
        }

        [Fact]
        public async void Save_SetDateModifiedValue()
        {
            var leaveType = new LeaveType
            {
                Id = 6,
                Name = "Annual Leave123",
                DefaultDays = 10,
            };

            await _context.LeaveTypes.AddAsync(leaveType);
            await _context.SaveChangesAsync();

            leaveType.UpdatedAt.ShouldNotBeNull();
        }
    }
}