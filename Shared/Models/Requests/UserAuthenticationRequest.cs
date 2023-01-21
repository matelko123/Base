namespace Shared.Models.Requests;

public class UserAuthenticationRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}