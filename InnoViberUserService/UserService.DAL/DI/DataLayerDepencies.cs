using UserService.DAL.Data;
using Microsoft.Extensions.DependencyInjection;
using UserService.DAL.Interfaces;
using UserService.DAL.Repositories;

namespace UserService.DAL.DI;

public static class DataLayerDepencies
{
    public static void RegisterDALDependencies(this IServiceCollection services)
    {
        services.AddSingleton<MongoDbContext>();
        services.AddTransient<IUserRepository, UserRepository>();
    }
}
