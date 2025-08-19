using HRLeaveManagement.Application.Contracts.Loggin;
using Microsoft.Extensions.Logging;

namespace HRLeaveManagement.Infrastructure.Loggin;

public class LoggerAdaper<T> : IAppLogger<T>
{
    private readonly ILogger<T> _logger;

    public LoggerAdaper(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<T>();
    }
    public void LogInformation(string message, params object[] args)
    {
        if (args == null || args.Length == 0)
        {
            _logger.LogInformation(message);
        }
        else
        {
            _logger.LogInformation(message, args);
        }
    }

    public void LogWarning(string message, params object[] args)
    {
        if (args == null || args.Length == 0)
        {
            _logger.LogWarning(message);
        }
        else
        {
            _logger.LogWarning(message, args);
        }
    }

    public void LogError(string message, Exception exception = null)
    {
        if (exception == null)
        {
            _logger.LogError(message);
        }
        else
        {
            _logger.LogError(exception, message);
        }
    }

    public void LogDebug(string message)
    {
        _logger.LogDebug(message);
    }

    public void LogCritical(string message, Exception exception = null)
    {
        if (exception == null)
        {
            _logger.LogCritical(message);
        }
        else
        {
            _logger.LogCritical(exception, message);
        }
    }
}