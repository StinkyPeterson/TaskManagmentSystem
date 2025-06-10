using Application.Infrastructure.Contracts;
using Domain.Entities;
using Infrastructure.Postgres.Persistence;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Infrastructure.Postgres.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<User> GetById(Guid id, CancellationToken token = default)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, token);
    }

    public async Task Create(User obj, CancellationToken token = default)
    {
        await _dbContext.AddAsync(obj, token);
    }

    public void Update(User obj)
    {
        _dbContext.Update(obj);
    }

    public void DeleteUser(User user)
    {
        _dbContext.Users.Remove(user);
    }

    public async Task<User> GetByUserNameOrEmail(string userName, string email, CancellationToken token = default)
    {
        return await _dbContext.Users.Where(u => u.UserName == userName || u.Email == email).FirstOrDefaultAsync(token);
    }

    public async Task<IReadOnlyCollection<User>> GetUserList(int page, int pageSize, CancellationToken token = default)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Skip((page - 1 ) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);
    }

    public async Task<bool> ExistsById(Guid id, CancellationToken token = default)
    {
        return await _dbContext.Users.AnyAsync(u => u.Id == id, token);
    }

    public async Task SaveChanges(CancellationToken token = default)
    {
        await _dbContext.SaveChangesAsync(token);
    }
}