using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class GetTaskListRequestValidator: AbstractValidator<GetTaskListRequest>
{
    public GetTaskListRequestValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Указан недопустимый статус задачи")
            .When(x => x.Status.HasValue);

        RuleFor(x => x.AssignedUserId)
            .NotEmpty().WithMessage("ID пользователя не может быть пустым")
            .When(x => x.AssignedUserId.HasValue);

        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("Номер страницы должен быть положительным числом");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("Размер страницы должен быть положительным числом");

        RuleFor(x => x.SortBy)
            .IsInEnum().WithMessage("Указано недопустимое поле для сортировки");

        RuleFor(x => x.SortDir)
            .IsInEnum().WithMessage("Указано недопустимое направление сортировки");
    }
}