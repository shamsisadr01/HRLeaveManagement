using AutoMapper;
using HRLeaveManagement.Mvc.UI.Contracts;
using HRLeaveManagement.Mvc.UI.Models.LeaveType;
using HRLeaveManagement.Mvc.UI.Services.Base;

namespace HRLeaveManagement.Mvc.UI.Services;

public class LeaveTypeService : BaseHttpService, ILeaveTypeService
{
    private readonly IMapper _mapper;
    public LeaveTypeService(IClient client,IMapper mapper) : base(client)
    {
        _mapper = mapper;
    }

    public async Task<List<LeaveTypeVM>> GetAllAsync()
    {
        var leaveTypes = await _client.LeaveTypesAllAsync();
        var data = _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
        return data;
    }

    public async Task<LeaveTypeVM> GetByIdAsync(int id)
    {
        var leaveType = await _client.LeaveTypesGETAsync(id);
        return _mapper.Map<LeaveTypeVM>(leaveType);
    }

    public async Task<Response<Guid>> CreateAsync(LeaveTypeVM leaveType)
    {
        try
        {
            var mappedLeaveType = _mapper.Map<CreateLeaveTypeCommand>(leaveType);
            await _client.LeaveTypesPOSTAsync(mappedLeaveType);
            return new Response<Guid>
            {
                Success = true,
                Message = "Leave type created successfully."
            };
        }
        catch (ApiException e)
        {
            return ConvertApiExceptions(e);
        }
    }

    public async Task<Response<Guid>> UpdateAsync(int id,LeaveTypeVM leaveType)
    {
        try
        {
            var updateLeaveType = _mapper.Map<UpdateLeaveTypeCommand>(leaveType);
            await _client.LeaveTypesPUTAsync(id+"",updateLeaveType);
            return new Response<Guid>
            {
                Success = true,
                Message = "Leave type Updated successfully."
            };
        }
        catch (ApiException e)
        {
            return ConvertApiExceptions(e);
        }
    }

    public async Task<Response<Guid>> DeleteAsync(int id)
    {
        try
        {
            await _client.LeaveTypesDELETEAsync(id);
            return new Response<Guid>
            {
                Success = true,
                Message = "Leave type Deleted successfully."
            };
        }
        catch (ApiException e)
        {
            return ConvertApiExceptions(e);
        }
    }
}