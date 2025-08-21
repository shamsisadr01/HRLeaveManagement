using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using Moq;

namespace HRLeaveManagement.Applications.UnitTests.Mocks;

public class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetLeaveTypesMockLeaveTypeRepository()
    {
        var leaveTypes = new List<LeaveType>
        {
            new LeaveType
            {
                Id = 1,
                Name = "Annual Leave",
                DefaultDays = 20,
            },
            new LeaveType
            {
                Id = 2,
                Name = "Sick Leave",
                DefaultDays = 10,
            }
        };

        var mock = new Mock<ILeaveTypeRepository>();

        mock.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(leaveTypes);

        mock.Setup(repo => repo.AddAsync(It.IsAny<LeaveType>()))
            .Returns((LeaveType leaveType) =>
            {
                leaveTypes.Add(leaveType);
                return Task.CompletedTask;
            });

        return mock;
    }
}