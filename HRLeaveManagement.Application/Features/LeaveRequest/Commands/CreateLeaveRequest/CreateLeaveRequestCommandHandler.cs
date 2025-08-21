using AutoMapper;
using FluentValidation.Results;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Loggin;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Models.Email;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler:IRequestHandler<CreateLeaveRequestCommand,Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmailSender _emailSender;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<CreateLeaveRequestCommandHandler> _appLogger;
    private readonly IMapper _mapper;

    public CreateLeaveRequestCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IEmailSender emailSender, ILeaveRequestRepository leaveRequestRepository, IAppLogger<CreateLeaveRequestCommandHandler> appLogger)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _emailSender = emailSender;
        _leaveRequestRepository = leaveRequestRepository;
        _appLogger = appLogger;
    }

    public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
        {
            throw new BadRequestExceptions("Invalid Leave Request", validationResult);
        }

        var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
        await _leaveRequestRepository.AddAsync(leaveRequest);

        try
        {
            var email = new EmailMessage
            {
                To = String.Empty,
                Subject = "Leave Request Updated",
                Body = $"Your leave request for {leaveRequest.StartDate.ToShortDateString()} to {leaveRequest.EndDate.ToShortDateString()} has been updated."
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception e)
        {
            _appLogger.LogWarning(e.Message);
        }
        return Unit.Value;
    }
}