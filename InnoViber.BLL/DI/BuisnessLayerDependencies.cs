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
        services.AddAutoMapper(typeof(BllLayerMapperProfile));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IMessageService, MessageService>();
    }
}
