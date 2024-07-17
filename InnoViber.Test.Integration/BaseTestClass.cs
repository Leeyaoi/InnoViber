using dotenv.net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Net.Http.Json;

namespace InnoViber.Test.Integration;

public class BaseTestClass : IClassFixture<DataBaseWebApplicationFactory>
{
    protected readonly HttpClient _client;

    public BaseTestClass(DataBaseWebApplicationFactory factory)
    {
        _client = factory.WebHost.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,

        });
    }

    protected async Task<TViewModel> AddModelToDatabase<TViewModel, TCreationModel>(string endpoint, TCreationModel data, CancellationToken ct)
    {
        var responseCreatingModel = await _client.PostAsJsonAsync(endpoint, data, options: null, ct);
        Console.WriteLine(responseCreatingModel);
        var json = await responseCreatingModel.Content.ReadAsStringAsync();
        Console.WriteLine(json);
        var createdModelString = await responseCreatingModel.Content.ReadFromJsonAsync<TViewModel>(ct);
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