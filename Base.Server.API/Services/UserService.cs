using Base.Server.API.Models.Entities;
using Base.Server.API.Repositories;
using Mapster;
using Shared.Models.Requests;
using Shared.Models.Response;

namespace Base.Server.API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IReadOnlyCollection<UserDto>> GetAll()
    {
        IReadOnlyCollection<User> users = await _userRepository.GetAll();
        return users.Adapt<IReadOnlyCollection<UserDto>>();
    }

    public async Task<UserDto?> GetById(Guid userId)
    {
        User? user = await _userRepository.GetById(userId);
        return user?.Adapt<UserDto>();
    }

    public async Task<UserDto?> GetByUsername(string username)
    {
        User? user = await _userRepository.GetByUsername(username);
        return user?.Adapt<UserDto>();
    }

    public async Task<UserDto?> Add(UserRegistrationRequest user)
    {
        User? createdUser = await _userRepository.Add(user.Adapt<User>());
        return createdUser?.Adapt<UserDto>();
    }

    public async Task<UserDto?> Update(UserUpdateRequest user)
    {
        User? updateUser = await _userRepository.GetById(user.Id);
        if (updateUser is null)
        {
            return null;
        }

        user.Adapt(updateUser);
        User? updatedUser = await _userRepository.Update(updateUser);
        return updatedUser?.Adapt<UserDto>();
    }

    public async Task<bool> Delete(Guid userId)
    {
        return await _userRepository.Delete(userId);
    }
}