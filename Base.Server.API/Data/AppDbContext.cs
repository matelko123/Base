using Microsoft.EntityFrameworkCore;

namespace Base.Server.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
}