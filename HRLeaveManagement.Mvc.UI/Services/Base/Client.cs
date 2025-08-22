namespace HRLeaveManagement.Mvc.UI.Services.Base;

public partial class Client : IClient
{
    public HttpClient HttpClient
    {
        get
        {
            return _httpClient ?? throw new InvalidOperationException("HttpClient is not initialized. Please ensure it is set before accessing it.");
        }
    }
}

