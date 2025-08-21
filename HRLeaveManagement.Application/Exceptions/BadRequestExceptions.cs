using FluentValidation.Results;

namespace HRLeaveManagement.Application.Exceptions;

public class BadRequestExceptions : Exception
{
    public BadRequestExceptions(string message) : base(message)
    {

    }

    public BadRequestExceptions(string message,ValidationResult validation) : base(message)
    {
        Errors = validation.ToDictionary();
    }

    public IDictionary<string, string[]> Errors { get; set; }
}