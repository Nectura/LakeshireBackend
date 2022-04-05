namespace LakeshireAuth.Models.Requests;

public sealed class RefreshTokenRequest
{
    public string UserId { get; set; } = "";
    public string RefreshToken { get; set; } = "";
}