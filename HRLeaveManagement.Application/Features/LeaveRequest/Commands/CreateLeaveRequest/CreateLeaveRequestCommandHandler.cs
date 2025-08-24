using AutoMapper;
using FluentValidation.Results;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Identity;
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
    private readonly IUserService _userService;
    private readonly ILeaveAllocationRepository _allocationRepository;
    public CreateLeaveRequestCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IEmailSender emailSender, ILeaveRequestRepository leaveRequestRepository, IAppLogger<CreateLeaveRequestCommandHandler> appLogger, IUserService userService)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _emailSender = emailSender;
        _leaveRequestRepository = leaveRequestRepository;
        _appLogger = appLogger;
        _userService = userService;
    }

    public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.Errors.Any())
        {
            throw new BadRequestExceptions("Invalid Leave Request", validationResult);
        }

        var employeeId = _userService.UserId;
        var allocation = await _allocationRepository.GetUserAllocation(request.LeaveTypeId, employeeId);

        if (allocation is null)
        {
            validationResult.Errors.Add(new ValidationFailure
                (nameof(request.LeaveTypeId),"you do not have any allocations for this leave type"));
            throw new BadRequestExceptions("Invalid Leave Request", validationResult);
        }

        var dayRequests = (int)(request.EndDate - request.StartDate).TotalDays;
        if (dayRequests > allocation.NumberOfDays)
        {
            validationResult.Errors.Add(new ValidationFailure
                (nameof(request.LeaveTypeId), "you do not have any allocations for this leave type"));
            throw new BadRequestExceptions("Invalid Leave Request", validationResult);
        }

        var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
        leaveRequest.RequestingEmployeeId = employeeId;
        leaveRequest.DateRequested = DateTime.Now;
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