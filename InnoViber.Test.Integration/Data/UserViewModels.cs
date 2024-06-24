using InnoViber.API.ViewModels.User;

namespace InnoViber.Test.Integration.Data;

public static class UserViewModels
{
    public static UserShortViewModel ShortUser = new()
    {
        Name = "Test",
        Surname = "Test",
        Email = "example@mail.com"
    };

    public static UserViewModel User = new()
    {
        Id = Guid.NewGuid(),
        Name = "Test",
        Surname = "Test",
        Email = "example@mail.com"
    };
}
