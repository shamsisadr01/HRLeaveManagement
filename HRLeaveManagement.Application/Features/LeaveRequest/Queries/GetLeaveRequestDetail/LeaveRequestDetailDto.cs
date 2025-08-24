﻿using HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HRLeaveManagement.Application.Models.Identity;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class LeaveRequestDetailDto
{
    public int Id { get; set; }
    public Employee Employee { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }


    public int LeaveTypeId { get; set; }
    public LeaveTypeDto? LeaveType { get; set; }

    public DateTime DateRequested { get; set; }
    public string? RequestComments { get; set; }

    public bool? Approved { get; set; }

    public bool Cancelled { get; set; }

    public string? RequestingEmployeeId { get; set; } = string.Empty;
}