using HRLeaveManagement.Mvc.UI.Contracts;
using HRLeaveManagement.Mvc.UI.Services.Base;

namespace HRLeaveManagement.Mvc.UI.Services
{
    public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
    {
        public LeaveAllocationService(IClient client) : base(client)
        {
        }
    }
}
