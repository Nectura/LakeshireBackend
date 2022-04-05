namespace Lakeshire.Common.Configs;

public class JwtAuthConfig
{
    public string Issuer { get; set; } = "";
    public string Audience { get; set; } = "";
    public string PrivateKey { get; set; } = "";
    public bool ValidateAudience { get; set; }
    public bool ValidateIssuer { get; set; }
    public int AccessTokenLifeSpanInMinutes { get; set; }
    public int RefreshTokenLifeSpanInMinutes { get; set; }
}