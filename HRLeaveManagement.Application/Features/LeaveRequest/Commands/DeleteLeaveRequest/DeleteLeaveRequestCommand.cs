using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommand : IRequest
{
    public DeleteLeaveRequestCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}