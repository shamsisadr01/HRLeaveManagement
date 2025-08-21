﻿using HRLeaveManagement.Application.Features.LeaveRequest.Shared;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class LeaveRequestCommand : BaseLeaveRequest, IRequest<Unit>
{
    public int Id { get; set; }
    public string? RequestComments { get; set; }

    public bool Cancelled { get; set; }
}