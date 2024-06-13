using FluentValidation;
using InnoViber.API.ViewModels.Chat;

namespace InnoViber.API.Validators;

public class ChatViewModelValidation : AbstractValidator<ChatShortViewModel>
{
    public ChatViewModelValidation()
    {
        RuleFor(x => x.Name).Length(1, 10);
    }
}
