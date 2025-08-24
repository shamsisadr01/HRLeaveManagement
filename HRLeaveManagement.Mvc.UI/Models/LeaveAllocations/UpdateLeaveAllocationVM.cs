using System.ComponentModel.DataAnnotations;
using HRLeaveManagement.Mvc.UI.Models.LeaveType;

namespace HRLeaveManagement.Mvc.UI.Models.LeaveAllocations;

public class UpdateLeaveAllocationVM
{
    public int Id { get; set; }

    [Display(Name = "Number Of Days")]
    [Range(1, 50, ErrorMessage = "Enter Valid Number")]
    public int NumberOfDays { get; set; }
    public LeaveTypeVM LeaveType { get; set; }

}