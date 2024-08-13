namespace SharedModels;

public interface IUserInfo
{
    string UserName { get; }
    string Email { get; }
    double HowLong { get; }
    string AuthorName { get; }
    string ChatName { get; }
}
