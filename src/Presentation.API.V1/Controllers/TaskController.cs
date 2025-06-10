using Application.Application.Contracts;
using Application.DTOs;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.V1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    /// <summary>
    /// Получить задачу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("get-by-id")]
    public async Task<ActionResult<TaskItem>> GetTaskById([FromQuery] Guid id, CancellationToken token)
    {
        return Ok(await _taskService.GetOneById(id, token));
    }

    /// <summary>
    /// Получить все задачи пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("get-task-by-user")]
    public async Task<ActionResult<IReadOnlyCollection<TaskViewModel>>> GetTaskByUser([FromQuery] Guid userId, CancellationToken token)
    {
        return Ok(await _taskService.GetTaskByUser(userId, token));
    }

    /// <summary>
    /// Получить список задач
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("get-task-list")]
    public async Task<ActionResult<TaskViewModel>> GetTaskList([FromQuery] GetTaskListRequest request, CancellationToken token)
    {
        return Ok(await _taskService.GetTaskList(request, token));
    }
    
    /// <summary>
    /// Создать задачу
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPost("create-task")]
    public async Task<ActionResult> CreateTask([FromBody] CreateTaskRequest request, CancellationToken token)
    {
        return Ok(await _taskService.CreateTask(request, token));
    }

    /// <summary>
    /// Обновить задачу
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPut("update-task")]
    public async Task<ActionResult> UpdateTask([FromBody] UpdateTaskRequest request, CancellationToken token)
    {
        await _taskService.UpdateTask(request, token);
        return Ok();
    }

    /// <summary>
    /// Обновить статус задачи
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPut("update-task-status")]
    public async Task<ActionResult> UpdateTaskStatus([FromBody] UpdateTaskStatusRequest request, CancellationToken token)
    {
        await _taskService.UpdateTaskStatus(request, token);
        return Ok();
    }
    
    /// <summary>
    /// Назначить задачу пользователю
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPut("assign-task")]
    public async Task<ActionResult> AssignTask([FromBody] AssignTaskRequest request, CancellationToken token)
    {
        await _taskService.AssignTaskToUser(request, token);
        return Ok();
    }

    /// <summary>
    /// Удалить задачу
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpDelete("delete-task")]
    public async Task<ActionResult> DeleteTask([FromQuery] Guid id, CancellationToken token)
    {
        await _taskService.DeleteTaskById(id, token);
        return Ok();
    }
    
}