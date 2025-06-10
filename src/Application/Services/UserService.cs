using Application.Application.Contracts;
using Application.DTOs;
using Application.Exceptions;
using Application.Infrastructure.Contracts;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Task = System.Threading.Tasks.Task;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<User> GetOneById(Guid id, CancellationToken token = default)
    {
        return await _userRepository.GetById(id, token);
    }

    public async Task<Guid> CreateUser(CreateUserRequest request, CancellationToken token = default)
    {
        User user = new User(Guid.NewGuid(), request.UserName, request.Email);
        var existingDb = await _userRepository.GetByUserNameOrEmail(user.UserName, user.Email, token);
        if (existingDb is not null)
        {
            throw new InvalidOperationException("Пользователь с таким Username или Email уже существует!");
        }

        await _userRepository.Create(user, token);
        await _userRepository.SaveChanges(token);
        return user.Id;
    }

    public async Task UpdateUser(UpdateUserRequest request, CancellationToken token = default)
    {
        var existingUser = await GetOneById(request.Id, token);
        if (existingUser is null)
        {
            throw new EntityNotFoundException("Пользователь не найден");
        }

        if (request.UserName is not null)
        {
            var existingUserName = await _userRepository.GetByUserNameOrEmail(request.UserName, String.Empty, token);
            if (existingUserName is not null)
            {
                throw new DuplicateEntityException("Пользователь с таким UserName существует!");
            }
            existingUser.SetUserName(request.UserName);
        }

        if (request.Email is not null)
        {
            var existingEmail = await _userRepository.GetByUserNameOrEmail(string.Empty, request.Email, token);
            if (existingEmail is not null)
            {
                throw new DuplicateEntityException("Пользователь с таким Email существует!");
            }
            existingUser.SetEmail(request.Email);
        }
        
        _userRepository.Update(existingUser);
        await _userRepository.SaveChanges(token);
    }

    public async Task DeleteUser(Guid id, CancellationToken token = default)
    {
        var existingUser = await GetOneById(id, token);
        if (existingUser is null)
        {
            throw new EntityNotFoundException("Пользователь не найден");
        }
        _userRepository.DeleteUser(existingUser);
        await _userRepository.SaveChanges(token);
    }

    public async Task<IReadOnlyCollection<UserViewModel>> GetUserList(int page, int pageSize, CancellationToken token = default)
    {
        var userList = await _userRepository.GetUserList(page, pageSize, token);
        return _mapper.Map<IReadOnlyCollection<UserViewModel>>(userList);
    }

    public async Task<bool> ExistsById(Guid id, CancellationToken token = default)
    {
        return await _userRepository.ExistsById(id, token);
    }
}