using Application.DTOs;
using Domain.Entities;

namespace Application.Infrastructure.Contracts;

public interface ITaskRepository : IRepository<TaskItem>
{
    Task<IReadOnlyCollection<TaskItem>> GetTaskList(GetTaskListRequest request, CancellationToken token = default);
    Task<IReadOnlyCollection<TaskItem>> GetTaskByUser(Guid userId, CancellationToken token = default);
}