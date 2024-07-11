﻿using dotenv.net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace InnoViber.Test.Integration;

public class BaseTestClass : IClassFixture<DataBaseWebApplicationFactory>
{
    protected readonly HttpClient _client;

    public BaseTestClass(DataBaseWebApplicationFactory factory)
    {
        var config = InitConfiguration();
        _client = factory.WebHost.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,
        });
        _client.DefaultRequestHeaders.Add("Authorization", config["JWT_EXAMPLE"]);
    }

    protected async Task<TViewModel> AddModelToDatabase<TViewModel, TCreationModel>(string endpoint, TCreationModel data)
    {
        var responseCreatingModel = await _client.PostAsJsonAsync(endpoint, data);
        var createdModelString = await responseCreatingModel.Content.ReadFromJsonAsync<TViewModel>();
        return createdModelString!;
    }

    public static IConfiguration InitConfiguration()
    {
        DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { @".env" }));
        var config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();
        return config;
    }
}