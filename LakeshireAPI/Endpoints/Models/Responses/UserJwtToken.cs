namespace LakeshireAPI.Endpoints.Models.Responses;

public sealed class UserJwtToken
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}