using Domain.Entities;

namespace Application.Infrastructure.Contracts;

public interface IUserRepository : IRepository<User>
{
    void DeleteUser(User obj);
    Task<User> GetByUserNameOrEmail(string userName, string email, CancellationToken token = default);
    Task<IReadOnlyCollection<User>> GetUserList(int page, int pageSize, CancellationToken token = default);
    Task<bool> ExistsById(Guid id, CancellationToken token = default);
}