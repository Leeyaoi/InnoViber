using UserService.ExternalUsers.DAL.Interfaces;
using UserService.ExternalUsers.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace UserService.ExternalUsers.DAL.DI;

public static class UserDataLayerDepencies
{
    public static void RegisterUserDALDependencies(this IServiceCollection services)
    {
        services.AddHttpClient<IUserHttpService, UserHttpService>()
            .AddTransientHttpErrorPolicy(policy => policy.RetryAsync());
    }
}
