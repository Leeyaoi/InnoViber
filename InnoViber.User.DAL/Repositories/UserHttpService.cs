using InnoViber.User.DAL.Interfaces;
using InnoViber.User.DAL.Models;
using System.Net.Http.Json;
using System.Threading;

namespace InnoViber.User.DAL.Repositories;

public class UserHttpService : IUserHttpService
{
    private readonly HttpClient _httpClient;

    public UserHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7081/");
    }

    public Task<List<ExternalUserModel>?> GetAllUsers(CancellationToken ct)
    {
        return _httpClient.GetFromJsonAsync<List<ExternalUserModel>>("Users", cancellationToken: ct);
    }

    public Task<ExternalUserModel?> GetUser(Guid userId, CancellationToken ct)
    {
        return _httpClient.GetFromJsonAsync<ExternalUserModel>($"Users/{userId}", cancellationToken: ct);
    }
}
