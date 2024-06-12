using System;

namespace InnoViber.API.Middleware;

public class ExeptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILogger<ExeptionHandlerMiddleware> _logger;

    public ExeptionHandlerMiddleware(RequestDelegate next, ILogger<ExeptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            switch (ex.GetType())
            {
                default:
                    await HandleExeptionAsync(httpContext, ex);
                    break;
            }
        }
    }

    private Task HandleExeptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError("The problem occured {message}", exception.Message);
        context.Response.ContentType = "text/json";
        return context.Response.WriteAsync($"{exception.Message}\n{context.Response.StatusCode}");
    }
}

public static class ExeptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseExeptionHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExeptionHandlerMiddleware>();
    }
}
