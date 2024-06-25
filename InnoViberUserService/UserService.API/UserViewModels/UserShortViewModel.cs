using MongoDB.Bson;

namespace UserService.API.UserViewModels;

public class UserShortViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;
    public BsonBinaryData? UserPhoto { get; set; }
}
