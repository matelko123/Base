using Base.Server.API.Models.Entities;

namespace Base.Server.API.Repositories;

public interface IUserRepository
{
    Task<IReadOnlyCollection<User>> GetAll();
    Task<User?> GetById(Guid userId);
    Task<User?> GetByUsername(string username);

    Task<User?> Add(User user);
    Task<User?> Update(User user);
    Task<bool> Delete(Guid userId);
}