namespace Application.DTOs;

public record CreateUserRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
}