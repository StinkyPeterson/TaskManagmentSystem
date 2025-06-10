using Domain.Enums;

namespace Application.ViewModels;

public class TaskViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskItemStatus Status { get; set; }
    public string StatusName { get; set; } 
    public DateTime? DueDate { get; set; }
    public Guid? AssignedUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}