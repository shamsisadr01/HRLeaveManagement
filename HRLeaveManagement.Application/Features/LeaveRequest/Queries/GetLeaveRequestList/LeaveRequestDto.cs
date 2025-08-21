using HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class LeaveRequestDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public LeaveTypeDto? LeaveType { get; set; }

    public DateTime DateRequested { get; set; }

    public bool? Approved { get; set; }

    public string? RequestingEmployeeId { get; set; } = string.Empty;
}