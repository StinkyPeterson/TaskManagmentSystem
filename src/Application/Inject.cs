﻿using Application.Application.Contracts;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IUserService, UserService>()
            .AddScoped<ITaskService, TaskService>();
    }
}