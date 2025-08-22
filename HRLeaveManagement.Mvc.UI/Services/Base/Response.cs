namespace HRLeaveManagement.Mvc.UI.Services.Base;

public class Response<T>
{
    public T Data { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
    public string Error { get; set; }
}