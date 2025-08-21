using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocation;

public record GetAllLeaveAllocationQuery : IRequest<IReadOnlyList<LeaveAllocationDto>>;