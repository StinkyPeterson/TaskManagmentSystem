namespace Application.Contracts.DTOs;

public record CreateUserRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
}