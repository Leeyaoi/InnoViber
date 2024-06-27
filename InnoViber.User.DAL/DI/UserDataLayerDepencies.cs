using InnoViber.User.DAL.Interfaces;
using InnoViber.User.DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace InnoViber.User.DAL.DI;

public static class UserDataLayerDepencies
{
    public static void RegisterUserDALDependencies(this IServiceCollection services)
    {
        services.AddHttpClient<IUserHttpService, UserHttpService>()
            .AddTransientHttpErrorPolicy(policy => policy.RetryAsync());
    }
}
