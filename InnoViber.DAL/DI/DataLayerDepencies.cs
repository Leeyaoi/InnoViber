using InnoViber.DAL.Data;
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
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            option.UseNpgsql(connectionString);
        });
    }
}
