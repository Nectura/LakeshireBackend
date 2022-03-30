namespace Lakeshire.Common.Configs;

public class JwtAuthConfig
{
    public string Issuer { get; set; } = "";
    public string Audience { get; set; } = "";
    public string PrivateKey { get; set; } = "";
    public bool ValidateAudience { get; set; }
    public bool ValidateIssuer { get; set; }
    public int AccessTokenLifeSpanInSeconds { get; set; }
    public int RefreshTokenLifeSpanInSeconds { get; set; }
}