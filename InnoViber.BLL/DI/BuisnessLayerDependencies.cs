using AutoMapper.Extensions.ExpressionMapping;
using AutoMapper;
using InnoViber.BLL.Helpers;
using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InnoViber.BLL.DI;

public static class BuisnessLayerDependencies
{
    public static void RegisterBLLDependencies(this IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new Helper());
            cfg.AddExpressionMapping();
        });

        var mapper = config.CreateMapper();

        services.AddSingleton(mapper);

        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IChatService, ChatService>();
        services.AddTransient<IMessageService, MessageService>();
    }
}
