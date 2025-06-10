using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Название задачи обязательно для заполнения")
            .MaximumLength(200).WithMessage("Длина названия не должна превышать 200 символов");
        
        RuleFor(x => x.Description)
            .MaximumLength(2000).WithMessage("Описание не должно превышать 2000 символов")
            .When(x => !string.IsNullOrEmpty(x.Description));
        
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Указан недопустимый статус задачи")
            .When(x => x.Status.HasValue);
    }
    
}