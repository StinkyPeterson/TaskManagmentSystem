using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class CreateUserRequestValidator: AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Имя пользователя обязательно для заполнения")
            .MaximumLength(100).WithMessage("Имя пользователя не должно превышать 100 символов");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email обязателен для заполнения")
            .MaximumLength(255).WithMessage("Email не должен превышать 255 символов");
    }

}