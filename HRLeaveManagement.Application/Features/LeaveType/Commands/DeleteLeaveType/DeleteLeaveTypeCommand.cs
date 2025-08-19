using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class DeleteLeaveTypeCommand : IRequest<Unit>
{
   public int Id { get; set; }
}