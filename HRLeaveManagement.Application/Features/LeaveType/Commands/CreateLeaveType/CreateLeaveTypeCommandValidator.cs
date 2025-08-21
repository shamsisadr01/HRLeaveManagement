using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Leave type name is required.")
            .NotNull()
            .MaximumLength(70).WithMessage("Leave type name cannot exceed 70 characters.");

        RuleFor(x => x.DefaultDays)
            .GreaterThan(1).WithMessage("Default days must be greater than 1.")
            .LessThan(100).WithMessage("Default days must be less than 100.");

        RuleFor(x => x)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("Leave Type Already Exists");

    }

    private async Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token)
    {
        return await _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }
}