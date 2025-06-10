using Application.Application.Contracts;
using Application.DTOs;
using Application.Exceptions;
using Application.Infrastructure.Contracts;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public TaskService(ITaskRepository taskRepository, IUserService userService, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _userService = userService;
        _mapper = mapper;
    }
    
    public async Task<TaskItem> GetOneById(Guid id, CancellationToken token = default)
    {
        return await _taskRepository.GetById(id, token);
    }

    public async Task<Guid> CreateTask(CreateTaskRequest request, CancellationToken token = default)
    {
        await ValidateAssignedUser(request.AssignedUserId, token);
        
        TaskItem task = new TaskItem(Guid.NewGuid(), request.Title, request.Description, request.Status,
            request.DueDate, request.AssignedUserId);

        await _taskRepository.Create(task, token);
        await _taskRepository.SaveChanges(token);
        return task.Id;
    }

    public async Task UpdateTask(UpdateTaskRequest request, CancellationToken token = default)
    {
        var existingTask = await _taskRepository.GetById(request.Id, token);
        if (existingTask is null)
        {
            throw new EntityNotFoundException($"Задача с Id: {request.Id} не найдена");
        }
        
        await UpdateTaskEntity(request, token, existingTask);

        _taskRepository.Update(existingTask);
        await _taskRepository.SaveChanges(token);
    }

    public async Task DeleteTaskById(Guid id, CancellationToken token = default)
    {
        var taskInDb = await _taskRepository.GetById(id);
        if (taskInDb is null)
        {
            throw new EntityNotFoundException($"Задача с Id: {id} не найдена ");
        }
        taskInDb.DeleteTask();
        _taskRepository.Update(taskInDb);
        await _taskRepository.SaveChanges(token);
    }

    public async Task UpdateTaskStatus(UpdateTaskStatusRequest request, CancellationToken token = default)
    {
        var taskInDb = await _taskRepository.GetById(request.Id, token);
        if (taskInDb is null)
        {
            throw new EntityNotFoundException($"Задача с Id: {request.Id} не найдена ");
        }
        taskInDb.SetStatus(request.Status);
        _taskRepository.Update(taskInDb);
        await _taskRepository.SaveChanges(token);
    }

    public async Task<IReadOnlyCollection<TaskViewModel>> GetTaskList(GetTaskListRequest request, CancellationToken token = default)
    {
        var taskList = await _taskRepository.GetTaskList(request, token);
        return _mapper.Map<IReadOnlyCollection<TaskViewModel>>(taskList);
    }

    public async Task<IReadOnlyCollection<TaskViewModel>> GetTaskByUser(Guid userId, CancellationToken token = default)
    {
        var taskList = await _taskRepository.GetTaskByUser(userId, token);
        return _mapper.Map<IReadOnlyCollection<TaskViewModel>>(taskList);
    }

    public async Task AssignTaskToUser(AssignTaskRequest request, CancellationToken token = default)
    {
        var task = await _taskRepository.GetById(request.TaskId, token);
        if (task is null)
        {
            throw new EntityNotFoundException($"Задача с Id: {request.TaskId} не найдена");
        }

        await ValidateAssignedUser(request.UserId, token);

        task.SetAssignedUser(request.UserId);
        _taskRepository.Update(task);
        await _taskRepository.SaveChanges(token);    
    }
    
    private async Task ValidateAssignedUser(Guid? userId, CancellationToken token = default)
    {
        if (userId.HasValue && !await _userService.ExistsById(userId.Value, token))
        {
            throw new EntityNotFoundException($"Пользователь с Id: {userId.Value} не найден");
        }
    }
    
    private async Task UpdateTaskEntity(UpdateTaskRequest request, CancellationToken token, TaskItem existingTask)
    {
        if (request.AssignedUserId.HasValue)
        {
            await ValidateAssignedUser(request.AssignedUserId, token);
            existingTask.SetAssignedUser(request.AssignedUserId);
        }

        if (!string.IsNullOrEmpty(request.Title))
        {
            existingTask.SetTitle(request.Title);
        }

        if (request.Description is not null)
        {
            existingTask.SetDescription(request.Description);
        }

        if (request.Status.HasValue)
        {
            existingTask.SetStatus(request.Status);
        }

        if (request.DueDate is not null)
        {
            existingTask.SetDueDate(request.DueDate);
        }
    }

    
}