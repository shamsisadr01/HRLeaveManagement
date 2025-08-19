using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveTypeMustExist);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must not exceed 100 characters");
        RuleFor(x => x.DefaultDays)
            .GreaterThan(100).WithMessage("{PropertyName} must be greater than 100")
            .LessThan(1).WithMessage("{PropertyName} must be less than 1");

        RuleFor(p=>p)
            .MustAsync(LeaveTypeUniqueName)
            .WithMessage("Leave type name must be unique.");
    }

    private async Task<bool> LeaveTypeUniqueName(UpdateLeaveTypeCommand command, CancellationToken token)
    {
        return await _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }

    private async Task<bool> LeaveTypeMustExist(int Id, CancellationToken token)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(Id);
        if (leaveType == null)
        {
            return false;
        }
        return true;
    }
}