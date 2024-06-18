using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InnoViber.BLL.DI;

public static class BuisnessLayerDependencies
{
    public static void RegisterBLLDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<CheckIsSeenMessagesService>();
    }
}
