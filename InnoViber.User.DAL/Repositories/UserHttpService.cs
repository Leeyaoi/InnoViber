using InnoViber.User.DAL.Interfaces;
using InnoViber.User.DAL.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Threading;

namespace InnoViber.User.DAL.Repositories;

public class UserHttpService : IUserHttpService
{
    private readonly HttpClient _httpClient;

    public UserHttpService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        var baseUri = config.GetValue<string>("USERS_CONNECTION")!;
        _httpClient.BaseAddress = new Uri(baseUri);
    }

    public Task<ExternalUserModel?> GetUser(Guid userId, CancellationToken ct)
    {
        return _httpClient.GetFromJsonAsync<ExternalUserModel>($"/User/{userId}", cancellationToken: ct);
    }

    public Task<ExternalUserModel?> GetUserByAuthId(string authId, CancellationToken ct)
    {
        return _httpClient.GetFromJsonAsync<ExternalUserModel>($"/User/auth/{authId}", cancellationToken: ct);
    }

    public Task<HttpResponseMessage> PostUser(ShortExternalUserModel user, CancellationToken ct)
    {
        return _httpClient.PostAsJsonAsync($"/User", user, cancellationToken: ct);
    }
}
