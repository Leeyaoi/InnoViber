using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Services;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace InnoViber.BLL.DI;

public static class BuisnessLayerDependencies
{
    public static void RegisterBLLDependencies(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IChatService, ChatService>();
        services.AddTransient<IMessageService, MessageService>();
        services.AddTransient<IChatRoleService, ChatRoleService>();
        services.AddScoped<CheckIsSeenMessagesService>();
        services.AddHostedService<CheckIsSeenMessagesService>();
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost:15672", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ReceiveEndpoint("UserInfoQueue", e =>
                {
                    e.Bind("SharedModels:IUserInfo");
                });
            });
        });
    }
}
