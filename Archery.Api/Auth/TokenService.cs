using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Archery.Model;

namespace Archery.Api.Auth;

public class TokenService
{
    public string CreateToken(IdentityUser user, User simpleUser)
    {
        var expiration = DateTime.UtcNow.AddDays(1);
        var token = CreateJwtToken(
            CreateClaims(user, simpleUser),
            CreateSigningCredentials(),
            expiration
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private static JwtSecurityToken CreateJwtToken(List<Claim> claims,
                                            SigningCredentials credentials,
                                            DateTime expiration) =>
        new(
            null,
            null,
            claims,
            expires: expiration,
            signingCredentials: credentials
        );

    private static List<Claim> CreateClaims(IdentityUser user, User simpleUser)
    {
        try
        {
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, "UserAuthentication"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, simpleUser.Role),
                    new Claim("userId", simpleUser.Id.ToString())
                };
            return claims;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static SigningCredentials CreateSigningCredentials()
    {
        return new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    "sdl lajhödlakä-öasefGashaösiehfla.ishleuiagwkebfa,jksbngl.ailw.,jk.JKHL:KUJ:LJHBFALJUHÖ:OIh.igH:ILHG-ÖOJAÖ-POjöihR-GLAKENLGHABS;BDAJSERFILUABJRGJK:SDFN:YKLDNLXJ:FBN-Y:"
                )
            ),
            SecurityAlgorithms.HmacSha256
        );
    }
}
