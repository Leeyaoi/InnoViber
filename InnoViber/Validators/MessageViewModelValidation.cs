using FluentValidation;
using InnoViber.API.ViewModels.Message;

namespace InnoViber.API.Validators;

public class MessageViewModelValidation : AbstractValidator<MessageShortViewModel>
{
    public MessageViewModelValidation()
    {
        RuleFor(x => x.Date).NotNull().NotEmpty();
        RuleFor(x => x.Text).NotEmpty();
        RuleFor(x => x.Status).NotNull();
        RuleFor(x => x.ChatId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}
