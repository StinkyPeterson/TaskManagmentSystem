using Domain.Enums;

namespace Application.Contracts.DTOs;

public record CreateTaskRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskItemStatus? Status { get; set; }
    public DateTime? DueDate { get; set; }
    public Guid? AssignedUserId { get; set; }
}