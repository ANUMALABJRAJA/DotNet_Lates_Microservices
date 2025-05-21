using eCommerece.API.DTO;
using FluentValidation;

namespace eCommerece.API.Core.Validators{
    public class LoginRequestValidators : AbstractValidator<LoginRequest>{
            public LoginRequestValidators(){
                RuleFor(temp => temp.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Enter valid Email Address");
                RuleFor(temp => temp.Password).NotEmpty().WithMessage("Password is Must");
            }
    }
}