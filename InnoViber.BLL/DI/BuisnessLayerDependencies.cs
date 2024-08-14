using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Services;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace InnoViber.BLL.DI;

public static class BuisnessLayerDependencies
{
    public static void RegisterBLLDependencies(this IServiceCollection services)
    {
        services.AddTransient<IChatService, ChatService>();
        services.AddTransient<IMessageService, MessageService>();
        services.AddTransient<IChatRoleService, ChatRoleService>();
        services.AddScoped<CheckIsSeenMessagesService>();
        services.AddHostedService<CheckIsSeenMessagesService>();
    }
}
