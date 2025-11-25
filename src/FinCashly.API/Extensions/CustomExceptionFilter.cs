using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using FinCashly.Domain.Exceptions;

namespace FinCashly.API.Extensions;

public class CustomExceptionFilter : IExceptionFilter
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<CustomExceptionFilter> _logger;

    public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger, IWebHostEnvironment webHostEnvironment)
    {
        _logger = logger;
        _env = webHostEnvironment;
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        var response = exception switch
        {
            NotFoundException nf => new ProblemDetails
            {
                Title = "Resource not found",
                Status = (int)HttpStatusCode.NotFound,
                Detail = nf.Message
            },

            ValidationException ve => new ProblemDetails
            {
                Title = "Validation error",
                Status = (int)HttpStatusCode.BadRequest,
                Detail = ve.Message,
                Extensions = { ["errors"] = ve.Errors }
            },

            BusinessException be => new ProblemDetails
            {
                Title = "Business rule violation",
                Status = (int)HttpStatusCode.UnprocessableEntity,
                Detail = be.Message
            },

            _ => new ProblemDetails
            {
                Title = "Internal server error",
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = _env.IsDevelopment() ? exception.Message : "Unexpected error"
            }
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = response.Status
        };

        context.ExceptionHandled = true;
    }
}