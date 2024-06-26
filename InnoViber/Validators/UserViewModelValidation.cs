﻿using FluentValidation;
using InnoViber.API.ViewModels.User;

namespace InnoViber.API.Validators;

public class UserViewModelValidation : AbstractValidator<UserViewModel>
{
    public UserViewModelValidation()
    {
        RuleFor(x => x.MongoId).NotEmpty();
    }
}
