namespace Archery.Api.Auth;

public class AuthResponse
{
    public int Id { get; set; } // TODO remove

    public string Username { get; set; } = null!; // TODO remove

    public string Token { get; set; } = null!;

    public string Role { get; set; } = null!; // TODO remove
}