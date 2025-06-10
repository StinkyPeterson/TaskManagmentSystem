using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public sealed class TaskItem : Entity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public TaskItemStatus Status { get; private set; } = TaskItemStatus.New;
    public DateTime? DueDate { get; private set; }
    public Guid? AssignedUserId { get; private set; }   
    public User AssignedUser { get; private set; }
    
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; }
    public bool IsDeleted { get; protected set; }
    
    protected TaskItem(Guid id) : base(id)
    {
        
    }

    public TaskItem(Guid id, string title, string description, TaskItemStatus? status, DateTime? dueDate, Guid? assignedUserId) : base(id)
    {
        SetTitle(title);
        SetDescription(description);
        SetStatus(status);
        SetDueDate(dueDate);
        SetAssignedUser(assignedUserId);
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            throw new ArgumentException("Название задачи не может быть пустым");
        }
        Title = title;
        UpdateTimestamp();
    }
    
    public void SetDescription(string description)
    {
        Description = description;
        UpdateTimestamp();
    }

    public void SetStatus(TaskItemStatus? status)
    {
        if (status == null)
        {
            return;
        }
        Status = status.Value;
        UpdateTimestamp();
    }

    public void SetDueDate(DateTime? dueDate)
    {
        if(dueDate != null && dueDate.Value < DateTime.Now)
        {
            throw new ArgumentException("Срок окончания задачи не может быть меньше текущего времени");
        }
        DueDate = dueDate;
        UpdateTimestamp();
    }

    public void SetAssignedUser(Guid? assignedUserId)
    {
        AssignedUserId = assignedUserId;
        UpdateTimestamp();
    }

    public void DeleteTask()
    {
        IsDeleted = true;
        UpdateTimestamp();
    }
    
    protected void UpdateTimestamp()
    {
        UpdatedAt = DateTime.UtcNow;
    }
    
}