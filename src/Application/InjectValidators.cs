using Application.DTOs;
using Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class InjectValidators
{
    public static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IValidator<AssignTaskRequest>, AssignTaskRequestValidator>()
            .AddScoped<IValidator<CreateTaskRequest>, CreateTaskRequestValidator>()
            .AddScoped<IValidator<CreateUserRequest>, CreateUserRequestValidator>()
            .AddScoped<IValidator<GetTaskListRequest>, GetTaskListRequestValidator>()
            .AddScoped<IValidator<UpdateTaskRequest>, UpdateTaskRequestValidator>()
            .AddScoped<IValidator<UpdateTaskStatusRequest>, UpdateTaskStatusRequestValidator>()
            .AddScoped<IValidator<UpdateUserRequest>, UpdateUserRequestValidator>();

    }
}