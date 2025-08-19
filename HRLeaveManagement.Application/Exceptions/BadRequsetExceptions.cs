using FluentValidation.Results;

namespace HRLeaveManagement.Application.Exceptions;

public class BadRequsetExceptions : Exception
{
    public BadRequsetExceptions(string message) : base(message)
    {

    }

    public BadRequsetExceptions(string message,ValidationResult validation) : base(message)
    {
        Errors = validation.Errors.Select(e => e.ErrorMessage).ToList();
    }

    public List<string> Errors { get; set; }
}