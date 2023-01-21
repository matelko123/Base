using Base.Server.API.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Requests;
using Shared.Models.Response;

namespace Base.Server.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : Controller
{
    private readonly IJwtAuthenticationService _authenticationService;
    private readonly IUserService _userService;

    public AuthenticationController(IJwtAuthenticationService authenticationService, IUserService userService)
    {
        _authenticationService = authenticationService;
        _userService = userService;
    }

    [HttpPost("Authenticate")]
    public async Task<ActionResult<UserAuthenticationResponse>> Authenticate([FromBody] UserAuthenticationRequest user)
    {
        UserAuthenticationResponse? token = await _authenticationService.Authenticate(user);
        return token is null
            ? Unauthorized()
            : Ok(token);
    }


    [HttpPost("Register")]
    public async Task<ActionResult<UserDto>> Register([FromBody] UserRegistrationRequest user)
    {
        UserDto? addedUser = await _userService.Add(user);
        return addedUser is null
            ? BadRequest()
            : Created($"/{addedUser.Id}", addedUser);
    }
}