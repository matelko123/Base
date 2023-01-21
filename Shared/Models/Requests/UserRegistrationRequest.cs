namespace Shared.Models.Requests;

public class UserRegistrationRequest
{
    public required string Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}