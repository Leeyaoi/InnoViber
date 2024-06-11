﻿using AutoMapper;
using InnoViber.API.Helpers;

namespace InnoViber.API.DI;

public static class ApiLayerDependencies
{
    public static void RegisterAPIDependencies(this IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new Helper());
        });

        var mapper = config.CreateMapper();

        services.AddSingleton(mapper);
    }
}