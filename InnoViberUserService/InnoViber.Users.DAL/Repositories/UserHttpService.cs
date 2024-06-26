using UserService.ExternalUsers.DAL.Interfaces;
using UserService.ExternalUsers.DAL.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Threading;

namespace UserService.ExternalUsers.DAL.Repositories;

public class UserHttpService : IUserHttpService
{
    private readonly HttpClient _httpClient;

    public UserHttpService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        var baseUri = config.GetConnectionString("UsersConnection");
        _httpClient.BaseAddress = new Uri(baseUri);
    }

    public Task<HttpResponseMessage> CreateUser(ExternalUserModel model, CancellationToken ct)
    {
        return _httpClient.PostAsJsonAsync<ExternalUserModel>("Users", model, cancellationToken: ct);
    }
}
