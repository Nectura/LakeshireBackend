using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;

namespace LakeshireAuth.Services;

public sealed class JwtAuthService
{
    private readonly IOptions<JwtAuthConfig> _jwtAuthConfig;

    public JwtAuthService(IOptions<JwtAuthConfig> jwtAuthConfig)
    {
        _jwtAuthConfig = jwtAuthConfig;
    }
    
    public JwtToken GenerateUserJwtToken(string userId)
    {
        var securityToken = ConstructJwtSecurityToken(userId);
        return new JwtToken
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(securityToken),
            RefreshToken = StringExtensions.GenerateCryptoSafeString(),
            ExpirationTime = securityToken.ValidTo
        };
    }

    private JwtSecurityToken ConstructJwtSecurityToken(string subject)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, subject),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAuthConfig.Value.PrivateTokenKey));

        var securityToken = new JwtSecurityToken(
            issuer: _jwtAuthConfig.Value.Issuer,
            audience: _jwtAuthConfig.Value.Audience,
            expires: DateTime.UtcNow.Add(AuthConfig.JwtLifeSpanTillRefreshIsNeeded),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return securityToken;
    }
}