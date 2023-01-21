using System.ComponentModel.DataAnnotations;

namespace Base.Server.API.Models;

public class JSONWebTokensSettings
{
    [Required]
    public required string Key { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    [Required]
    public required double DurationInMinutes { get; set; }
}