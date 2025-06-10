using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
{
    public UpdateTaskRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID задачи обязателен для заполнения")
            .NotEqual(Guid.Empty).WithMessage("ID задачи не может быть пустым");
    }
}