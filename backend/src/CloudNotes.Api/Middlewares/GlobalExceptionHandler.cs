using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using CloudNotes.Domain.Exceptions.Common;

namespace CloudNotes.Api.Middlewares;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> _logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ProblemDetails problemDetails = new()
        {
            Instance = httpContext.Request.Path,
            Title = exception.Message,
        };

        _logger.LogError("{ProblemDetailsTitle}", problemDetails.Title);

        if (exception is BaseException ex)
        {
            httpContext.Response.StatusCode = (int)ex.StatusCode;
            problemDetails.Title = ex.Message;
        }

        problemDetails.Status = httpContext.Response.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);

        return true;
    }
}
