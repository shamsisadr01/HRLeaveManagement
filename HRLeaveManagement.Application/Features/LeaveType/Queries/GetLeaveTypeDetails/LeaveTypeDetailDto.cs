namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class LeaveTypeDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int DefaultDays { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}