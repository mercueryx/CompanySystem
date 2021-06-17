using Consignment_Control.Models.Login;
using System;
using System.Collections.Generic;
using FluentValidation;
using System.Linq;
using System.Web;

namespace Consignment_Control.Validation.Login
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}