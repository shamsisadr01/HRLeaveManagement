using HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocation;

public class LeaveAllocationDto
{
    public int Id { get; set; }

    public int NumberOfDays { get; set; }

    public int Period { get; set; }

    public int LeaveTypeId { get; set; }

    public LeaveTypeDto? LeaveType { get; set; }
}