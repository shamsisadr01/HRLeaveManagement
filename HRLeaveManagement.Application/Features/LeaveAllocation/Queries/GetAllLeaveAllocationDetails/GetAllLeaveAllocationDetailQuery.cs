using HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocation;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocationDetails;

public record GetAllLeaveAllocationDetailQuery(int Id) : IRequest<LeaveAllocationDetailsDto>;