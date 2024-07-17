using UserService.BLL.Helper;
using UserService.DAL.DI;
using UserService.BLL.DI;
using dotenv.net;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
        builder.Services.AddAutoMapper(typeof(BllLayerMapperProfile).Assembly, typeof(Program).Assembly);

        builder.Services.RegisterDALDependencies();
        builder.Services.RegisterBllDependencies();

        var app = builder.Build();

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

        app.UseAuthorization();


        app.MapControllers();

        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.Run();
    }
}
