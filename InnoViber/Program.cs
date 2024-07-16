using InnoViber.DAL.DI;
using InnoViber.User.DAL.DI;
using InnoViber.BLL.DI;
using InnoViber.API.DI;
using InnoViber.Domain.DI;
using InnoViber.API.Extensions;
using MassTransit;
using Microsoft.Net.Http.Headers;
using InnoViber.API.Middleware;
using dotenv.net;

namespace InnoViber;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { @".env" }));

        builder.Configuration.AddEnvironmentVariables();

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

        builder.Services.AddControllersWithViews();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        builder.Services.RegisterDALDependencies(builder.Configuration);

        builder.Services.RegisterBLLDependencies();

        builder.RegisterAPIDependencies();

        builder.Services.RegisterDomainDependencies();

        builder.Services.RegisterUserDALDependencies();

        builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(BuisnessLayerDependencies).Assembly);

        var app = builder.Build();

        app.UseExeptionHandlerMiddleware();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(settings =>
            {
                settings.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1.0");
                settings.OAuthClientId(builder.Configuration.GetValue<string>("AUTH0_CLIENT_ID"));
                settings.OAuthClientSecret(builder.Configuration.GetValue<string>("AUTH0_CLIENT_SECRET"));
                settings.OAuthUsePkce();
            });
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseSecureHeaders();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action}/{id?}");

        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseAuthentication();
        app.UseAuthorization();

        app.Run();
    }
}
