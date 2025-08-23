using AutoMapper;
using HRLeaveManagement.Application.Contracts.Identity;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IUserService userService)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _userService = userService;
    }

    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
        var validationResult = validator.Validate(request);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestExceptions("Invalid Leave Allocation Request", validationResult);
        }

        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

        var employees = await _userService.GetEmployeesAsync();

        var period = DateTime.Now.Year;

        var allocations = new List<Domain.LeaveAllocation>();

        foreach (var employee in employees)
        {
            var allocationExit = await _leaveAllocationRepository.AllocationExists(request.LeaveTypeId, employee.Id.ToString(),period);
            if (allocationExit == false)
            {
                allocations.Add(new Domain.LeaveAllocation()
                {
                    Id = employee.Id,
                    LeaveTypeId = request.LeaveTypeId,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = period,
                });
            }
        }

        if (allocations.Any())
        {
            await _leaveAllocationRepository.AddAllocations(allocations);
        }

      //  var leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(request);
      //  await _leaveAllocationRepository.AddAsync(leaveAllocation);

        return Unit.Value;
    }
}