using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.Api.Models;

public class CustomValidationProblemDetails : ProblemDetails
{
    public IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();
}