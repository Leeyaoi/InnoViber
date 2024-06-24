using Microsoft.Extensions.DependencyInjection;
using UserService.BLL.Interfaces;
using UserService.BLL.Services;

namespace UserService.BLL.DI;

public static class BllLayerDependencies
{
    public static void RegisterBllDependencies(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UsersService>();
    }
}
