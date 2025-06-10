using Application.DTOs;
using Application.DTOs.Enums;
using Application.Infrastructure.Contracts;
using Domain.Entities;
using Infrastructure.Postgres.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public TaskRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<TaskItem> GetById(Guid id, CancellationToken token = default)
    {
        return await _dbContext.Tasks.AsNoTracking().Include(u => u.AssignedUser).FirstOrDefaultAsync(t => t.Id == id, token);
    }

    public async Task Create(TaskItem obj, CancellationToken token = default)
    {
        await _dbContext.AddAsync(obj, token);
    }

    public void Update(TaskItem obj)
    {
        _dbContext.Update(obj);
    }
    public async Task<IReadOnlyCollection<TaskItem>> GetTaskList(GetTaskListRequest request, CancellationToken token = default)
    {
        var query = _dbContext.Tasks.AsQueryable();

        if (request.Status.HasValue)
        {
            query = query.Where(t => t.Status == request.Status.Value);
        }

        if (request.AssignedUserId.HasValue)
        {
            query = query.Where(t => t.AssignedUserId == request.AssignedUserId.Value);
        }

        query = query.Where(t => !t.IsDeleted);
        
        query = request.SortBy switch
        {
            TaskSortField.DueDate => request.SortDir == SortDirection.Desc
                ? query.OrderByDescending(t => t.DueDate)
                : query.OrderBy(t => t.DueDate),
            TaskSortField.Title => request.SortDir == SortDirection.Desc
                ? query.OrderByDescending(t => t.Title)
                : query.OrderBy(t => t.Title),
            TaskSortField.Status => request.SortDir == SortDirection.Desc
                ? query.OrderByDescending(t => t.Status)
                : query.OrderBy(t => t.Status),

            _ => request.SortDir == SortDirection.Desc
                ? query.OrderByDescending(t => t.DueDate)
                : query.OrderBy(t => t.DueDate)
        };
        
        return await query
            .AsNoTracking()
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(token);

    }

    public async Task<IReadOnlyCollection<TaskItem>> GetTaskByUser(Guid userId, CancellationToken token = default)
    {
        return await _dbContext.Tasks.AsNoTracking().Where(t => t.AssignedUserId == userId && !t.IsDeleted).ToListAsync(token);
    }

    public async Task SaveChanges(CancellationToken token = default)
    {
        await _dbContext.SaveChangesAsync(token);
    }
    

}