using Application.Contracts.DTOs;
using Domain.Entities;

namespace Application.Contracts.Interfaces;

public interface ITaskService
{
    Task<TaskItem> GetOneById(Guid id);
    Task CreateTask(CreateTaskRequest request);
    Task UpdateTask(UpdateTaskRequest request);
    Task DeleteTaskById(Guid id);
    Task UpdateTaskStatus(UpdateTaskStatusRequest request);
    Task<IReadOnlyCollection<TaskItem>> GetTaskList(GetTaskListRequest request);
    Task<IReadOnlyCollection<TaskItem>> GetTaskByUser(Guid userId);
    Task AssignTaskToUser(AssignTaskRequest request);
}