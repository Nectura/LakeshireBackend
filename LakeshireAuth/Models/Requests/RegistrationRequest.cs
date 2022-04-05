namespace LakeshireAuth.Models.Requests;

public sealed class RegistrationRequest
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string EmailAddress { get; set; } = "";
    public string Password { get; set; } = "";
    public int Gender { get; set; }
}