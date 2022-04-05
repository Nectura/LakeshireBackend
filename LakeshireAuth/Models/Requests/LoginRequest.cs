namespace LakeshireAuth.Models.Requests;

public sealed class LoginRequest
{
    public string EmailAddress { get; set; } = "";
    public string Password { get; set; } = "";
}