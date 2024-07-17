using InnoViber.Domain.Enums;

namespace InnoViber.API.ViewModels.ChatRole;

public class ChatRoleShortViewModel
{
    public UserRoles Role { get; set; }
    public string UserId { get; set; } = string.Empty;
    public Guid ChatId { get; set; }
}
