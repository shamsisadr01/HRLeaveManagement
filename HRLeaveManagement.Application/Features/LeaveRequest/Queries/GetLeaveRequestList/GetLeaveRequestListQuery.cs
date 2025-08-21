using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public record GetLeaveRequestListQuery : IRequest<IReadOnlyList<LeaveRequestDto>>;