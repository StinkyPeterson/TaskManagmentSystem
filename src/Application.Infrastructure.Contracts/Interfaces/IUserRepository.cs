using Domain.Entities;

namespace Application.Infrastructure.Contracts.Interfaces;

public interface IUserRepository : IRepository<User>
{
    void DeleteUser(User obj);
    Task<User> GetByUserNameOrEmail(string userName, string email);
    Task<IReadOnlyCollection<User>> GetUserList(int skip, int take);
    Task<bool> ExistsById(Guid id);
}