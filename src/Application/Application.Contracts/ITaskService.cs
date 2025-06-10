using Application.DTOs;
using Application.ViewModels;
using Domain.Entities;

namespace Application.Application.Contracts;

public interface ITaskService
{
    Task<TaskItem> GetOneById(Guid id, CancellationToken token = default);
    Task<Guid> CreateTask(CreateTaskRequest request, CancellationToken token = default);
    Task UpdateTask(UpdateTaskRequest request, CancellationToken token = default);
    Task DeleteTaskById(Guid id, CancellationToken token = default);
    Task UpdateTaskStatus(UpdateTaskStatusRequest request, CancellationToken token = default);
    Task<IReadOnlyCollection<TaskViewModel>> GetTaskList(GetTaskListRequest request, CancellationToken token = default);
    Task<IReadOnlyCollection<TaskViewModel>> GetTaskByUser(Guid userId, CancellationToken token = default);
    Task AssignTaskToUser(AssignTaskRequest request, CancellationToken token = default);
}