using dotenv.net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace InnoViber.Test.Integration;

public class BaseTestClass : IClassFixture<DataBaseWebApplicationFactory>
{
    protected readonly HttpClient _client;

    public BaseTestClass(DataBaseWebApplicationFactory factory)
    {
        _client = factory.WebHost.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("http://localhost:3000"),
            AllowAutoRedirect = false,

        });
    }

    protected async Task<TViewModel> AddModelToDatabase<TViewModel, TCreationModel>(string endpoint, TCreationModel data)
    {
        var responseCreatingModel = await _client.PostAsJsonAsync(endpoint, data);
        Console.WriteLine(responseCreatingModel);
        var json = await responseCreatingModel.Content.ReadAsStringAsync();
        Console.WriteLine(json);
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