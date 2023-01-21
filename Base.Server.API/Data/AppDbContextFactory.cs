using Microsoft.EntityFrameworkCore;

namespace Base.Server.API.Data;

public class AppDbContextFactory
{
    private readonly DbContextOptions<AppDbContext> _options;

    public AppDbContextFactory(DbContextOptions<AppDbContext> options)
    {
        _options = options;
    }

    public AppDbContext Create() => new(_options);
}