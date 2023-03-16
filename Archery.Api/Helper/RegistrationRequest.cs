using System.ComponentModel.DataAnnotations;

namespace Archery.Api.Helper;
public class RegistrationRequest
{
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}