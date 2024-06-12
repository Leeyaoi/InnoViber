using AutoMapper;
using InnoViber.API.Helpers;
using Serilog;

namespace InnoViber.API.DI;

public static class ApiLayerDependencies
{
    public static void RegisterAPIDependencies(this WebApplicationBuilder builder)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new Helper());
        });

        var mapper = config.CreateMapper();

        builder.Services.AddSingleton(mapper);

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        builder.Logging.AddSerilog().SetMinimumLevel(LogLevel.Information);
    }
}
