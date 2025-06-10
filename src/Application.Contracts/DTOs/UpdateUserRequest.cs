namespace Application.Contracts.DTOs;

public record UpdateUserRequest
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
}