namespace Archery.Api.Helper;

public class AuthResponse
{
    public string Username { get; set; } = null!;

    public string Token { get; set; } = null!;

    public string Role { get; set; } = null!;
}