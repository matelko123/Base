using Base.Server.API.Extensions;
using Base.Server.API.Middlewares;
using Base.Server.API.Repositories;
using Base.Server.API.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Configuration
    builder.Services.RegisterConfiguration();

    // DbContext
    builder.RegisterDbContext();

    // Extensions
    builder.Services.RegisterMapsterConfiguration();

    // Services
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IJwtAuthenticationService, JwtAuthenticationService>();
    builder.Services.AddScoped<ExceptionMiddleware>();

    // Authentication
    builder.RegisterAuthentication();
}


WebApplication app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionMiddleware>();
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}