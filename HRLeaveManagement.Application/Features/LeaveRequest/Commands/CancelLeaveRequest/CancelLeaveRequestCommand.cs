using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommand : IRequest<Unit>
{
    public CancelLeaveRequestCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}