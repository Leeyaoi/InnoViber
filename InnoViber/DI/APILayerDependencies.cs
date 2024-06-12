using Serilog;
using FluentValidation;
using InnoViber.API.ViewModels.Chat;

namespace InnoViber.API.DI;

public static class ApiLayerDependencies
{
    public static void RegisterAPIDependencies(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        builder.Logging.AddSerilog().SetMinimumLevel(LogLevel.Information);

        builder.Services.AddValidatorsFromAssemblyContaining<ChatShortViewModel>();
    }
}
