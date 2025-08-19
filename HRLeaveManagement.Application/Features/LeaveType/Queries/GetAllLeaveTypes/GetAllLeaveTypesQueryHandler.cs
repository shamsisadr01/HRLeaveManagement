using AutoMapper;
using HRLeaveManagement.Application.Contracts.Loggin;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetAllLeaveTypesQueryHandler : IRequestHandler<GetAllLeaveTypesQuery, IEnumerable<Domain.LeaveType>>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetAllLeaveTypesQueryHandler> _logger;

    public GetAllLeaveTypesQueryHandler(ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper,IAppLogger<GetAllLeaveTypesQueryHandler> logger)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<IEnumerable<Domain.LeaveType>> Handle(GetAllLeaveTypesQuery request, CancellationToken cancellationToken)
    {
        var leaveTypes = await _leaveTypeRepository.GetAllAsync();

        var data = _mapper.Map<IEnumerable<Domain.LeaveType>>(leaveTypes);

        _logger.LogInformation("Retrieved {Count} leave types", data.Count());
        return data;
    }
}