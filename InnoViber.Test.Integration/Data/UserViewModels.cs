using InnoViber.API.ViewModels.User;

namespace InnoViber.Test.Integration.Data;

public static class UserViewModels
{
    public static UserShortViewModel ShortUser = new()
    {
        MongoId = Guid.NewGuid(),
    };

    public static UserViewModel User = new()
    {
        Id = Guid.NewGuid(),
        MongoId = Guid.NewGuid(),
    };
}
