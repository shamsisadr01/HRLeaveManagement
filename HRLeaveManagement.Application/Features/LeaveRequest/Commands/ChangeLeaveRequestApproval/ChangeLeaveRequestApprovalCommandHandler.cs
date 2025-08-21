using AutoMapper;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Loggin;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Models.Email;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmailSender _emailSender;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<ChangeLeaveRequestApprovalCommandHandler> _appLogger;
    private readonly IMapper _mapper;

    public ChangeLeaveRequestApprovalCommandHandler(ILeaveRequestRepository leaveRequestRepository,
        IEmailSender emailSender, ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<ChangeLeaveRequestApprovalCommandHandler> appLogger, IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _emailSender = emailSender;
        _leaveTypeRepository = leaveTypeRepository;
        _appLogger = appLogger;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
        if (leaveRequest is null)
        {
            _appLogger.LogError($"Leave request with ID {request.Id} not found.");
            throw new NotFoundExceptions(nameof(leaveRequest),request.Id);
        }

        leaveRequest.Approved = request.Approved;
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