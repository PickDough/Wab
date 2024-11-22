using System.Net;
using Wab.Core.Domain.Exception;

namespace Wab.Api.Exception;

public class CoreExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public CoreExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (CoreException ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, CoreException exception)
    {
        context.Response.StatusCode = exception.Detail switch
        {
            CoreExceptionDetail.NotFound => (int)HttpStatusCode.NotFound,
            CoreExceptionDetail.Unauthorized => (int)HttpStatusCode.Unauthorized,
            _ => (int)HttpStatusCode.InternalServerError
        };
        await context.Response.WriteAsync(exception.ToString());
    }
}