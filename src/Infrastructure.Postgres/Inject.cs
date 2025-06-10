using Application.Infrastructure.Contracts;
using Infrastructure.Postgres.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Postgres;

public static class Inject
{
    public static IServiceCollection AddInfrasctructure(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<ITaskRepository, TaskRepository>()
            .AddScoped<IUserRepository, UserRepository>();
    }
}