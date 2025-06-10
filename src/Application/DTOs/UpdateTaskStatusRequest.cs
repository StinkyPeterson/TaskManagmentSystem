using Domain.Enums;

namespace Application.DTOs;

public record UpdateTaskStatusRequest
{
    public Guid Id { get; set; }
    public TaskItemStatus Status { get; set; }
}