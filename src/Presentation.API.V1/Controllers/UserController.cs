using Application.Application.Contracts;
using Application.DTOs;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.V1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    /// <summary>
    /// Получить пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("get-by-id")]
    public async Task<ActionResult<User>> GetUserById([FromQuery] Guid id, CancellationToken token)
    {
        return Ok(await _userService.GetOneById(id, token));
    }
    
    /// <summary>
    /// Получить список пользователей
    /// </summary>
    /// <param name="page">Номер страницы</param>
    /// <param name="pageSize">Количество элементов на странице</param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("get-user-list")]
    public async Task<ActionResult<IReadOnlyCollection<UserViewModel>>> GetUserList([FromQuery] int page, int pageSize, CancellationToken token)
    {
        return Ok(await _userService.GetUserList(page, pageSize, token));
    }

    /// <summary>
    /// Создать пользователя
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPost("create-user")]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken token)
    {
        return Ok(await _userService.CreateUser(request, token));
    }

    /// <summary>
    /// Обновить пользователя
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPut("update-user")]
    public async Task<ActionResult> UpdateUser([FromBody] UpdateUserRequest request, CancellationToken token)
    {
        await _userService.UpdateUser(request, token);
        return Ok();
    }

    /// <summary>
    /// Удалить пользователя
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpDelete("delete-user")]
    public async Task<ActionResult> DeleteUser([FromQuery] Guid id, CancellationToken token)
    {
        await _userService.DeleteUser(id, token);
        return Ok();
    }
    
}