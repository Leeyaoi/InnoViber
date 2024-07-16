using dotenv.net;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace InnoViberGateway;

public class Program
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

        builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

        builder.Services.AddOcelot(builder.Configuration);

        builder.Services.AddSwaggerForOcelot(builder.Configuration);

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "API Gateway",
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
                    Implicit = new OpenApiOAuthFlow{
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

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerForOcelotUI();
        }

        app.UseOcelot().Wait();

        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.Run();

    }
}


