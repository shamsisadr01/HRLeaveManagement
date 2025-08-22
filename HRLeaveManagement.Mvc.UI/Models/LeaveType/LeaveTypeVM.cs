using System.ComponentModel.DataAnnotations;

namespace HRLeaveManagement.Mvc.UI.Models.LeaveType;

public class LeaveTypeVM
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    [Required]
    [Display(Name = "Default Days")]
    public int DefaultDays { get; set; }
}