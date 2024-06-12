using FluentValidation;
using InnoViber.API.ViewModels.User;

namespace InnoViber.API.Validators;

public class UserViewModelValidation : AbstractValidator<UserShortViewModel>
{
    public UserViewModelValidation()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.Email).Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
    }
}
