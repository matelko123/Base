using Base.Server.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Requests;
using Shared.Models.Response;

namespace Base.Server.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<UserDto>> GetById([FromRoute] Guid userId)
    {
        UserDto? user = await _userService.GetById(userId);
        return user is null
            ? NotFound()
            : Ok(user);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<UserDto>> GetByUsername([FromRoute] string username)
    {
        UserDto? user = await _userService.GetByUsername(username);
        return user is null
            ? NotFound()
            : Ok(user);
    }

    [HttpPut]
    public async Task<ActionResult<UserDto>> Update([FromBody] UserUpdateRequest user)
    {
        UserDto? updatedUser = await _userService.Update(user);
        return updatedUser is null
            ? BadRequest()
            : Ok(updatedUser);
    }

    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid userId)
    {
        bool isDeleted = await _userService.Delete(userId);
        return isDeleted
            ? Ok()
            : BadRequest();
    }
}