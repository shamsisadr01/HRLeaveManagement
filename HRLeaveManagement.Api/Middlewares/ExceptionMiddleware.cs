using System.Net;
using HRLeaveManagement.Api.Models;
using HRLeaveManagement.Application.Exceptions;
using SendGrid.Helpers.Errors.Model;

namespace HRLeaveManagement.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext content)
    {
        try
        {
            await _next(content);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(content, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext content, Exception exception)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        CustomValidationProblemDetails problem = new();

        switch (exception)
        {
            case BadRequestExceptions badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                problem = new CustomValidationProblemDetails()
                {
                    Title = badRequestException.Message,
                    Errors = badRequestException.Errors,
                    Status = (int)statusCode,
                    Type = nameof(BadRequestExceptions),
                    Detail = badRequestException.InnerException?.Message
                };
                break;
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                problem = new CustomValidationProblemDetails()
                {
                    Title = notFoundException.Message,
                    Status = (int)statusCode,
                    Type = nameof(NotFoundException),
                    Detail = notFoundException.InnerException?.Message
                };
                break;
            default:
                problem = new CustomValidationProblemDetails()
                {
                    Title = exception.Message,
                    Status = (int)statusCode,
                    Type = nameof(HttpStatusCode.InternalServerError),
                    Detail = exception.StackTrace
                };
                break;
        }

        content.Response.StatusCode = (int)statusCode;
        await content.Response.WriteAsJsonAsync(problem);
    }
}