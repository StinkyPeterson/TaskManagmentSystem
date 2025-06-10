using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class UpdateTaskStatusRequestValidator : AbstractValidator<UpdateTaskStatusRequest>
{
    public UpdateTaskStatusRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID задачи обязателен для заполнения")
            .NotEqual(Guid.Empty).WithMessage("ID задачи не может быть пустым");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Указан недопустимый статус задачи");
    }
}