using AutoMapper.Extensions.ExpressionMapping;
using InnoViber.BLL.Helpers;
using InnoViber.BLL.Interfaces;
using InnoViber.BLL.Models;
using InnoViber.BLL.Services;
using InnoViber.DAL.Interfaces;
using InnoViber.DAL.Models;
using InnoViber.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace InnoViber.BLL.DI;

public static class BuisnessLayerDependencies
{
    public static void RegisterBLLDependencies(this IServiceCollection services)
    {
        var config = new AutoMapper.MapperConfiguration(cfg =>
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
