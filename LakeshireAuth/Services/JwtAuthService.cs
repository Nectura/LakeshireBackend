using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
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
    
    public async Task<UserJwtToken> RefreshUserAccessTokenAsync(string userId, string refreshToken, string[]? scopes = null, CancellationToken cancellationToken = default)
    {
        var userAuth = await _authWorkUnit.UserAccountAuths
            .Query(m => m.UserId == userId && m.RefreshToken == refreshToken)
            .Include(m => m.User)
            .FirstOrDefaultAsync(cancellationToken);

        if (userAuth?.User == null)
            throw new FileNotFoundException();

        if (DateTime.UtcNow >= userAuth.AbsoluteExpirationTime)
            throw new InvalidCredentialException();

        userAuth.RefreshToken = _authService.GenerateRandomCryptoSafeString();
        userAuth.AbsoluteExpirationTime = DateTime.UtcNow.AddSeconds(_jwtAuthConfig.Value.RefreshTokenLifeSpanInSeconds);

        await _authWorkUnit.CommitChangesAsync(cancellationToken);

        return new UserJwtToken
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(ConstructJwtSecurityToken(userAuth.User)),
            RefreshToken = userAuth.RefreshToken
        };
    }
    
    public async Task<UserJwtToken> CreateUserAccessTokenAsync(UserAccount userAccount, string[]? scopes = null, CancellationToken cancellationToken = default)
    {
        var userAuth = new UserAccountServiceAuth
        {
            User = userAccount,
            RefreshToken = _authService.GenerateRandomCryptoSafeString(),
            AbsoluteExpirationTime = DateTime.UtcNow.AddSeconds(_jwtAuthConfig.Value.RefreshTokenLifeSpanInSeconds)
        };
        
        _authWorkUnit.UserAccountAuths.Add(userAuth);
        
        await _authWorkUnit.CommitChangesAsync(cancellationToken);

        return new UserJwtToken
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(ConstructJwtSecurityToken(userAccount)),
            RefreshToken = userAuth.RefreshToken
        };
    }

    public async Task ExpireRefreshTokensAsync(UserAccount userAccount, CancellationToken cancellationToken = default)
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
            new(JwtRegisteredClaimNames.Sub, userAccount.Id),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAuthConfig.Value.PrivateKey));

        var securityToken = new JwtSecurityToken(
            issuer: _jwtAuthConfig.Value.Issuer,
            audience: _jwtAuthConfig.Value.Audience,
            expires: DateTime.UtcNow.AddSeconds(_jwtAuthConfig.Value.AccessTokenLifeSpanInSeconds),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return securityToken;
    }
}