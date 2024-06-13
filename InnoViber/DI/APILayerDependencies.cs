using Serilog;
using FluentValidation;
using static FluentValidation.DependencyInjectionExtensions;
using InnoViber.API.ViewModels.Chat;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace InnoViber.API.DI;

public static class ApiLayerDependencies
{
    public static void RegisterAPIDependencies(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        builder.Logging.AddSerilog().SetMinimumLevel(LogLevel.Information);

        builder.Services.AddFluentValidationAutoValidation();

        builder.Services.AddValidatorsFromAssemblyContaining<ChatShortViewModel>();
    }
}
