using Application.DTOs;
using Application.ViewModels;
using Domain.Entities;

namespace Application.Application.Contracts;

public interface IUserService
{
    Task<User> GetOneById(Guid id, CancellationToken token = default);
    Task<Guid> CreateUser(CreateUserRequest request, CancellationToken token = default);
    Task UpdateUser(UpdateUserRequest request, CancellationToken token = default);
    Task DeleteUser(Guid id, CancellationToken token = default);
    Task<IReadOnlyCollection<UserViewModel>> GetUserList(int page, int pageSize, CancellationToken token = default);
    Task<bool> ExistsById(Guid id, CancellationToken token = default);
}