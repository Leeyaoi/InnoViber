using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InnoViber.BLL.DI;

public static class BuisnessLayerDependencies
{
    public static void RegisterBLLDependencies(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IChatService, ChatService>();
        services.AddTransient<IMessageService, MessageService>();
        services.AddScoped<CheckIsSeenMessagesService>();
    }
}
