namespace LakeshireAuth.Models.Responses;

public sealed class UserJwtTokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}