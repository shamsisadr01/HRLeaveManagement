using HRLeaveManagement.Mvc.UI.Contracts;
using HRLeaveManagement.Mvc.UI.Services.Base;

namespace HRLeaveManagement.Mvc.UI.Services;

public class LeaveRequestService : BaseHttpService, ILeaveRequestService
{
    public LeaveRequestService(IClient client) : base(client)
    {
    }
}