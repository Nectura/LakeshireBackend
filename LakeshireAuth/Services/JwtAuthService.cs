using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lakeshire.Common.Configs;
using Lakeshire.Common.DAL.Models;
using Lakeshire.Common.DAL.Models.Interfaces;
using Lakeshire.Common.DAL.WorkUnits.Interfaces;
using LakeshireAuth.Models.Responses;
using LakeshireAuth.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LakeshireAuth.Services;

public sealed class JwtAuthService
{
    private readonly IOptions<JwtAuthConfig> _jwtAuthConfig;
    private readonly IAuthService _authService;
    private readonly IAuthWorkUnit _authWorkUnit;

    public JwtAuthService(
        IOptions<JwtAuthConfig> jwtAuthConfig,
        IAuthService authService,
        IAuthWorkUnit authWorkUnit)
    {
        _jwtAuthConfig = jwtAuthConfig;
        _authService = authService;
        _authWorkUnit = authWorkUnit;
    }
    
    // TODO: switch to the UserAccountServiceAuthTree, UserAccountServiceAuthTreeNode approach for enhanced security
    public async Task<UserJwtTokenResponse?> RefreshUserAccessTokenAsync(Guid userId, string refreshToken, string[]? scopes = null, CancellationToken cancellationToken = default)
    {
        var userAuth = await _authWorkUnit.UserAccountAuths
            .Query(m => m.UserId == userId && m.RefreshToken == refreshToken || m.UserId == userId && m.PreviousRefreshToken == refreshToken)
            .Include(m => m.User)
            .FirstOrDefaultAsync(cancellationToken);

        // Note: User Deletion Check, Absolute Lifetime Strategy, Refresh Token Automatic Reuse Detection
        if (userAuth?.User == default || DateTime.UtcNow >= userAuth.AbsoluteExpirationTime || userAuth.PreviousRefreshToken == refreshToken)
        {
            _authWorkUnit.UserAccountAuths.Remove(userAuth);
            await _authWorkUnit.CommitChangesAsync(cancellationToken);
            return default;
        }
        
        // Note: Refresh Token Rotation
        userAuth.RefreshToken = _authService.GenerateRandomCryptoSafeString();
        userAuth.AbsoluteExpirationTime = DateTime.UtcNow.AddMinutes(_jwtAuthConfig.Value.RefreshTokenLifeSpanInMinutes);

        await _authWorkUnit.CommitChangesAsync(cancellationToken);

        return new UserJwtTokenResponse
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(ConstructJwtSecurityToken(userAuth.User)),
            RefreshToken = userAuth.RefreshToken
        };
    }
    
    public async Task<UserJwtTokenResponse> CreateUserAccessTokenAsync(UserAccount userAccount, string[]? scopes = null, CancellationToken cancellationToken = default)
    {
        var userAuth = new UserAccountServiceAuth
        {
            User = userAccount,
            RefreshToken = _authService.GenerateRandomCryptoSafeString(),
            AbsoluteExpirationTime = DateTime.UtcNow.AddMinutes(_jwtAuthConfig.Value.RefreshTokenLifeSpanInMinutes)
        };
        
        _authWorkUnit.UserAccountAuths.Add(userAuth);
        
        await _authWorkUnit.CommitChangesAsync(cancellationToken);

        return new UserJwtTokenResponse
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(ConstructJwtSecurityToken(userAccount)),
            RefreshToken = userAuth.RefreshToken
        };
    }

    public async Task ExpireUserAccountRefreshTokensAsync(UserAccount userAccount, CancellationToken cancellationToken = default)
    {
        var userAuths = await _authWorkUnit.UserAccountAuths
            .Query(m => m.UserId == userAccount.Id)
            .ToListAsync(cancellationToken);

        foreach (var userAuth in userAuths)
            userAuth.AbsoluteExpirationTime = DateTime.UtcNow;

        await _authWorkUnit.CommitChangesAsync(cancellationToken);
    }

    private JwtSecurityToken ConstructJwtSecurityToken(IUserAccount userAccount)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userAccount.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAuthConfig.Value.PrivateKey));

        var securityToken = new JwtSecurityToken(
            issuer: _jwtAuthConfig.Value.Issuer,
            audience: _jwtAuthConfig.Value.Audience,
            expires: DateTime.UtcNow.AddMinutes(_jwtAuthConfig.Value.AccessTokenLifeSpanInMinutes),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return securityToken;
    }
}