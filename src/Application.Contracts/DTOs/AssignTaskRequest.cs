using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.DTOs;

public record AssignTaskRequest
{
    [Required]
    public Guid TaskId { get; set; }
    [Required]
    public Guid UserId { get; set; }
}