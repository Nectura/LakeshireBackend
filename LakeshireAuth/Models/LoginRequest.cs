using System.ComponentModel.DataAnnotations;

namespace LakeshireAuth.Models;

public sealed class LoginRequest
{
    [Required(ErrorMessage = $"The {nameof(EmailAddress)} field is required.")]
    [EmailAddress(ErrorMessage = $"The {nameof(EmailAddress)} is invalid.")]
    public string EmailAddress { get; set; } = "";
    
    [Required(ErrorMessage = $"The {nameof(Password)} field is required.")]
    public string Password { get; set; } = "";
}