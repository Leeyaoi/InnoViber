using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace InnoViberGateway;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

        builder.Services.AddOcelot(builder.Configuration);

        builder.Services.AddSwaggerForOcelot(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerForOcelotUI();
        }

        app.UseOcelot().GetAwaiter();

        app.Run();

    }
}


