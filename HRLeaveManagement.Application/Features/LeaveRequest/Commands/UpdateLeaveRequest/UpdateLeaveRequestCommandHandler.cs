using AutoMapper;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Loggin;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Models.Email;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommand : IRequestHandler<LeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<UpdateLeaveRequestCommand> _appLogger;
    private readonly IMapper _mapper;

    public UpdateLeaveRequestCommand(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper, IEmailSender emailSender, IAppLogger<UpdateLeaveRequestCommand> appLogger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
        _emailSender = emailSender;
        _appLogger = appLogger;
    }

    public async Task<Unit> Handle(LeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
        if (leaveRequest == null)
        {
            throw new NotFoundExceptions(nameof(LeaveRequest), request.Id);
        }

        var validator = new UpdateLeaveRequestCommandValidator(_leaveRequestRepository,_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Any())
        {
            throw new BadRequestExceptions("Invalid Leave Request",validationResult);
        }

        _mapper.Map(request, leaveRequest);

        await _leaveRequestRepository.UpdateAsync(leaveRequest);


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