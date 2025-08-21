using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocation;

public class
    GetAllLeaveAllocationQueryHandler : IRequestHandler<GetAllLeaveAllocationQuery, IReadOnlyList<LeaveAllocationDto>>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public GetAllLeaveAllocationQueryHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<LeaveAllocationDto>> Handle(GetAllLeaveAllocationQuery request, CancellationToken cancellationToken)
    {
        var LeaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsWithDetails();

        var allocations = _mapper.Map<IReadOnlyList<LeaveAllocationDto>>(LeaveAllocations);

        return allocations;
    }
}