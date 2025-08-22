namespace HRLeaveManagement.Mvc.UI.Services.Base
{
    public class BaseHttpService
    {
        private readonly IClient _client;

        public BaseHttpService(IClient client)
        {
            _client = client;
        }
    }
}
