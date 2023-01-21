using Shared.Models.Requests;
using Shared.Models.Response;

namespace Base.Server.API.Services;

public interface IUserService
{
    Task<IReadOnlyCollection<UserDto>> GetAll();
    Task<UserDto?> GetById(Guid userId);
    Task<UserDto?> GetByUsername(string username);

    Task<UserDto?> Add(UserRegistrationRequest user);
    Task<UserDto?> Update(UserUpdateRequest user);

    Task<bool> Delete(Guid userId);
}