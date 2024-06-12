using InnoViber.API.Middleware;

namespace InnoViber.API.Extensions;

public static class ExeptionHandlerMiddlewareExtension
{
    public static IApplicationBuilder UseExeptionHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExeptionHandlerMiddleware>();
    }
}
