namespace HRLeaveManagement.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }

    public string? CreateBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; } 

    public string? UpdateBy { get; set; }
}