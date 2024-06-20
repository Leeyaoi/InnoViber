using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace InnoViber.Test.Integration;

public class BaseTestClass : IClassFixture<DataBaseWebApplicationFactory<Program>>
{
    protected readonly HttpClient _client;

    public BaseTestClass(DataBaseWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    protected async Task<TViewModel> AddModelToDatabase<TViewModel, TCreationModel>(string endpoint, TCreationModel data)
    {
        var responseCreatingModel = await _client.PostAsJsonAsync(endpoint, data);
        var createdModelString = await responseCreatingModel.Content.ReadFromJsonAsync<TViewModel>();
        return createdModelString!;
    }
}