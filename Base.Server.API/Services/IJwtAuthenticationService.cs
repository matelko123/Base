using Shared.Models.Requests;
using Shared.Models.Response;

namespace Base.Server.API.Services;

public interface IJwtAuthenticationService
{
    public Task<UserAuthenticationResponse?> Authenticate(UserAuthenticationRequest userAuthenticationRequest);
}