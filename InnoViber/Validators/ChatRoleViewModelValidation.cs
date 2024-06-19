using FluentValidation;
using InnoViber.API.ViewModels.ChatRole;

namespace InnoViber.API.Validators;

public class ChatRoleViewModelValidation : AbstractValidator<ChatRoleViewModel>
{
    public ChatRoleViewModelValidation()
    {
        RuleFor(x => x.Role).NotNull();
        RuleFor(x => x.ChatId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}
