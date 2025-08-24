using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public record GetLeaveRequestListQuery(bool IsLoggedInUser) : IRequest<IReadOnlyList<LeaveRequestDto>>;