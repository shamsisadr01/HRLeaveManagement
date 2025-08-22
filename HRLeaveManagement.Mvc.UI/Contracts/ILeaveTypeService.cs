using HRLeaveManagement.Mvc.UI.Models.LeaveType;
using HRLeaveManagement.Mvc.UI.Services.Base;

namespace HRLeaveManagement.Mvc.UI.Contracts
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeVM>> GetAllAsync();
        Task<LeaveTypeVM> GetByIdAsync(int id);

        Task<Response<Guid>> CreateAsync(LeaveTypeVM leaveType);
        Task<Response<Guid>> UpdateAsync(int id,LeaveTypeVM leaveType);
        Task<Response<Guid>> DeleteAsync(int id);
    }
}
