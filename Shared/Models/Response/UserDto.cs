namespace Shared.Models.Response;

public class UserDto
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public bool IsEnabled { get; set; }

    public DateTime CreatedAtUtc { get; set; }
    public DateTime? LastModificationUtc { get; set; }

    public string FullName => $"{FirstName} {LastName}";
}