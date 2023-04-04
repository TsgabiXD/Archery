namespace Archery.Api.Auth;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            var path = httpContext.Request.Path;
            var token = string.Empty;
            var metaDataAddress = "https://www.ArcheryWebsite.com" + "/.well-known/oauth-authorization-server";
            CustomAuthHandler authHandler = new();
            var header = httpContext.Request.Headers["Authorization"];

            if (header.Count == 0) throw new Exception("Authorization header is empty");

            string[] tokenValue = Convert.ToString(header).Trim().Split(" ");

            if (tokenValue.Length > 1) token = tokenValue[1];
            else throw new Exception("Authorization token is empty");

            if (authHandler.IsValidToken(token, "https://www.ArcheryWebsite.com", "https://www.ArcheryWebsite.com", metaDataAddress))
                await _next(httpContext);
        }
        catch (Exception)
        {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            HttpResponseWritingExtensions.WriteAsync(httpContext.Response, "{\"message\": \"Unauthorized\"}").Wait();
        }
    }
}