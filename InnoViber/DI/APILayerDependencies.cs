using Serilog;
using FluentValidation;
using InnoViber.API.ViewModels.Chat;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using InnoViber.Controllers;

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

        var authority = $"https://{builder.Configuration["Auth0:Authority"]}/";

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = authority;
            options.Audience = builder.Configuration["Auth0:Audience"];
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuerSigningKey = true
            };
        });

        builder.Services.AddAuthorization();

        builder.Services.AddTransient<ChatRoleController>();
    }
}
