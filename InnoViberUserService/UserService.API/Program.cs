using UserService.BLL.Helper;
using UserService.DAL.DI;
using UserService.BLL.DI;
using dotenv.net;
using Microsoft.Net.Http.Headers;

namespace UserService.API;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { @".env" }));

        builder.Configuration.AddEnvironmentVariables();

        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(builder.Configuration.GetValue<string>("AUTH0_CLIENT_ORIGIN")!)
                    .WithHeaders(
                        HeaderNames.ContentType,
                        HeaderNames.Authorization
                    )
                    .AllowAnyMethod()
                    .SetPreflightMaxAge(TimeSpan.FromSeconds(86400));
            });
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(typeof(BllLayerMapperProfile).Assembly, typeof(Program).Assembly);

        builder.Services.RegisterDALDependencies();
        builder.Services.RegisterBllDependencies();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.Run();
    }
}
