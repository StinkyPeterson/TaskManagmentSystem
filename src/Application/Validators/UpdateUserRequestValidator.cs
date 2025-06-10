using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class UpdateUserRequestValidator :  AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID пользователя обязательно для заполнения")
            .NotEqual(Guid.Empty).WithMessage("ID пользователя не может быть пустым");
    }
}