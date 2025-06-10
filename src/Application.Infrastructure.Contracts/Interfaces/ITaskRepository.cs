using Domain.Entities;
using Domain.Enums;

namespace Application.Infrastructure.Contracts.Interfaces;

public interface ITaskRepository : IRepository<TaskItem>
{
    Task<IReadOnlyCollection<TaskItem>> GetTaskList(TaskItemStatus? status, Guid? userId, int sortBy, int sortDir, int page, int pageSize);
    Task<IReadOnlyCollection<TaskItem>> GetTaskByUser(Guid userId);
}