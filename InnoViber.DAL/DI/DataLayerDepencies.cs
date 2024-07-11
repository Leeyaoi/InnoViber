using InnoViber.DAL.Data;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace InnoViber.DAL.DI;

public static class DataLayerDepencies
{
    public static void RegisterDALDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ViberContext>(option =>
        {
            var connectionString = configuration.GetValue<string>("DB_CONNECTION");
            option.UseNpgsql(connectionString);
        }, ServiceLifetime.Transient);

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IChatRepository, ChatRepository>();
        services.AddTransient<IMessageRepository, MessageRepository>();
        services.AddTransient<IChatRoleRepository, ChatRoleRepository>();
    }
}
