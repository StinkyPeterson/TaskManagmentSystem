using Application.DTOs.Enums;
using Domain.Enums;

namespace Application.DTOs;

public record GetTaskListRequest
{
    public TaskItemStatus? Status { get; set; }
    public Guid? AssignedUserId { get; set; }

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public TaskSortField SortBy { get; set; } = TaskSortField.DueDate;
    public SortDirection SortDir { get; set; } = SortDirection.Asc;
}