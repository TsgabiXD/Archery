using System.ComponentModel.DataAnnotations;

namespace Archery.Api.Auth;

public class AuthRequest
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}