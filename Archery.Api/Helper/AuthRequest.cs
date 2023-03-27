using System.ComponentModel.DataAnnotations;

namespace Archery.Api.Helper;
public class AuthRequest
{
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;
}