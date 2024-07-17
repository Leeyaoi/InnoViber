namespace UserService.API.UserViewModels;

public class UserViewModel
{
    public Guid Id { get; set; }
    public string Auth0Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string NickName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;
    public string? UserPhoto { get; set; }
}
