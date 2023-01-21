using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Server.API.Models.Entities;

[Table("Users")]
public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MinLength(5)]
    public required string Username { get; set; }

    [Required]
    public required string Password { get; set; }

    [MinLength(3)]
    public string? FirstName { get; set; }

    [MinLength(3)]
    public string? LastName { get; set; }

    [EmailAddress]
    [MinLength(10)]
    public required string Email { get; set; }

    public bool IsEnabled { get; set; } = true;
}