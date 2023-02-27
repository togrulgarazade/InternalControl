using Business.ViewModels.Account;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Validators.Account
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(u => u.FullName).NotNull().NotEmpty().MaximumLength(255)
                .WithMessage("Zəhmət olmasa ad və soyad daxil edin !");
            RuleFor(u => u.UserName).NotNull().NotEmpty().MaximumLength(255)
                .WithMessage("Zəhmət olmasa istifadəçi adı daxil edin !");
            RuleFor(u => u.Email).NotNull().NotEmpty().MaximumLength(255).EmailAddress()
                .WithMessage("Zəhmət olmasa email daxil edin !");
            RuleFor(u => u.Password).NotNull().NotEmpty().MaximumLength(255)
                .WithMessage("Zəhmət olmasa şifrə daxil edin !");
            RuleFor(u => u.PasswordConfirm).NotNull().NotEmpty().MaximumLength(255)
                .WithMessage("Zəhmət olmasa şifrə daxil edin !");
        }
    }
}
