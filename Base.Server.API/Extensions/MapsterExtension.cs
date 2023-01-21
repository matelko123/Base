using Base.Server.API.Models.Entities;
using Mapster;
using Shared.Models.Requests;

namespace Base.Server.API.Extensions;

public static class MapsterExtension
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<UserUpdateRequest, User>.NewConfig().IgnoreNullValues(true);
    }
}