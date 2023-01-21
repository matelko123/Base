using Base.Server.API.Data;
using Base.Server.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Base.Server.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public UserRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IReadOnlyCollection<User>> GetAll()
    {
        await using AppDbContext context = await _contextFactory.CreateDbContextAsync();
        return await context.Users.ToListAsync();
    }

    public async Task<User?> GetById(Guid userId)
    {
        await using AppDbContext context = await _contextFactory.CreateDbContextAsync();
        User? user = await context.Users.SingleOrDefaultAsync(u => u.Id == userId);
        return user;
    }

    public async Task<User?> GetByUsername(string username)
    {
        await using AppDbContext context = await _contextFactory.CreateDbContextAsync();
        User? user = await context.Users.SingleOrDefaultAsync(u => u.Username == username);
        return user;
    }

    public async Task<User?> Add(User user)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user));

        // hash password
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        await using AppDbContext context = await _contextFactory.CreateDbContextAsync();
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> Delete(Guid userId)
    {
        await using AppDbContext context = await _contextFactory.CreateDbContextAsync();

        User? user = await GetById(userId);
        if (user is null)
        {
            return false;
        }

        context.Users.Remove(user);
        return await context.SaveChangesAsync() > 0;
    }


    public async Task<User?> Update(User user)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user));

        await using AppDbContext context = await _contextFactory.CreateDbContextAsync();
        context.Users.Update(user);
        await context.SaveChangesAsync();
        return user;
    }
}