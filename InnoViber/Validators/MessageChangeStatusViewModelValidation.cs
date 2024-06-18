using FluentValidation;
using InnoViber.API.ViewModels.Chat;
using InnoViber.API.ViewModels.Message;

namespace InnoViber.API.Validators;

public class MessageChangeStatusViewModelValidation : AbstractValidator<MessageChangeStatusViewModel>
{
    public MessageChangeStatusViewModelValidation()
    {
        RuleFor(x => x.Status).NotNull();
    }
}
