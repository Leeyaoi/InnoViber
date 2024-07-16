using Serilog;
using FluentValidation;
using InnoViber.API.ViewModels.Chat;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using InnoViber.Controllers;
using Microsoft.OpenApi.Models;

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

        var authority = $"https://{builder.Configuration.GetValue<string>("AUTH0_DOMAIN")}/";

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = authority;
            options.Audience = builder.Configuration.GetValue<string>("AUTH0_AUDIENCE");
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true
            };
        });

        builder.Services.AddAuthorization();

        builder.Services.AddTransient<ChatRoleController>();

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "API Documentation",
                Version = "v1.0",
                Description = ""
            });
            options.ResolveConflictingActions(x => x.First());
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                BearerFormat = "JWT",
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri($"https://{builder.Configuration.GetValue<string>("AUTH0_DOMAIN")}/oauth/token"),
                        AuthorizationUrl = new Uri($"https://{builder.Configuration.GetValue<string>("AUTH0_DOMAIN")}/authorize?audience={builder.Configuration.GetValue<string>("AUTH0_AUDIENCE")}"),
                        Scopes = new Dictionary<string, string> {
                        { "openid", "OpenId" },

                    }
                    }
                }
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                    },
                    new[] { "openid" }
                }
            });
        });
    }
}
