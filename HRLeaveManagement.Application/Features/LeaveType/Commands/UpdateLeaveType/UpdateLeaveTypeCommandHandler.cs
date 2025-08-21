using AutoMapper;
using HRLeaveManagement.Application.Contracts.Loggin;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger;

    public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper, IAppLogger<UpdateLeaveTypeCommandHandler> logger)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveTypeCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors occurred while updating leave type: {Errors}", validationResult.Errors);
            // یک پیام برای کلاس زیر
            throw new BadRequestExceptions("Validation errors occurred while updating leave type", validationResult);
        }
        var leaveType = _mapper.Map<Domain.LeaveType>(request);
        await _leaveTypeRepository.UpdateAsync(leaveType);

        return Unit.Value;
    }
}