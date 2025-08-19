namespace HRLeaveManagement.Application.Contracts.Loggin;

public interface IAppLogger<T>
{
    void LogInformation(string message,params object[] args);
    void LogWarning(string message, params object[] args);
    void LogError(string message, Exception exception = null);
    void LogDebug(string message);
    void LogCritical(string message, Exception exception = null);
}