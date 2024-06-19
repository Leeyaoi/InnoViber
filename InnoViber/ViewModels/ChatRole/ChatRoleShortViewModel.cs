using InnoViber.Domain.Enums;

namespace InnoViber.API.ViewModels.ChatRole;

public class ChatRoleShortViewModel
{
    public UserRoles Role { get; set; }
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }
}
