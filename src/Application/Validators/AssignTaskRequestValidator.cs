using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class AssignTaskRequestValidator: AbstractValidator<AssignTaskRequest>
{
    public AssignTaskRequestValidator()
    {
        RuleFor(x => x.TaskId)
            .NotEmpty().WithMessage("ID задачи обязателен для заполнения")
            .NotEqual(Guid.Empty).WithMessage("ID задачи не может быть пустым");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("ID пользователя обязателен для заполнения")
            .NotEqual(Guid.Empty).WithMessage("ID пользователя не может быть пустым");
    }
}