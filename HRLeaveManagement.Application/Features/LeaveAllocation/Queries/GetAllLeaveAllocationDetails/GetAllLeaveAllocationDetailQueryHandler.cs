using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocationDetails;

public class
    GetAllLeaveAllocationDetailQueryHandler : IRequestHandler<GetAllLeaveAllocationDetailQuery,
    LeaveAllocationDetailsDto>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public GetAllLeaveAllocationDetailQueryHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
    }

    public async Task<LeaveAllocationDetailsDto> Handle(GetAllLeaveAllocationDetailQuery request, CancellationToken cancellationToken)
    {
        var LeaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);

        var allocations = _mapper.Map<LeaveAllocationDetailsDto>(LeaveAllocations);

        return allocations;
    }
}