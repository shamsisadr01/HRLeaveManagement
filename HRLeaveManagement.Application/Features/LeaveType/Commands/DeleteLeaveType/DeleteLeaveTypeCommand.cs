using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommand : IRequest<Unit>
{
    public DeleteLeaveTypeCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }

}