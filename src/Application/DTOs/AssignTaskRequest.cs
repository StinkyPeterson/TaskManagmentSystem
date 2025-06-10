namespace Application.DTOs;

public record AssignTaskRequest
{
    public Guid TaskId { get; set; }
    public Guid UserId { get; set; }
}