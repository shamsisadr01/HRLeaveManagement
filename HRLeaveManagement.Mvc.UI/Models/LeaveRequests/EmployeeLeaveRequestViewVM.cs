using HRLeaveManagement.Mvc.UI.Models.LeaveAllocations;

namespace HRLeaveManagement.Mvc.UI.Models.LeaveRequests;

public class EmployeeLeaveRequestViewVM
{
    public List<LeaveAllocationVM> LeaveAllocations { get; set; } = new List<LeaveAllocationVM>();
    public List<LeaveRequestVM> LeaveRequests { get; set; } = new List<LeaveRequestVM>();
}