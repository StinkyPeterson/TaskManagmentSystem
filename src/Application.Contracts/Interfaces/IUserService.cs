using Application.Contracts.DTOs;
using Domain.Entities;

namespace Application.Contracts.Interfaces;

public interface IUserService
{
    Task<User> GetOneById(Guid id);
    Task CreateUser(CreateUserRequest request);
    Task UpdateUser(UpdateUserRequest request);
    Task DeleteUser(Guid id);
    Task<IReadOnlyCollection<User>> GetUserList(int skip, int take);
    Task<bool> ExistsById(Guid id);
}