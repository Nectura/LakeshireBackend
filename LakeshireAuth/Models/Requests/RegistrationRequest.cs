using System.ComponentModel.DataAnnotations;
using Lakeshire.Common.Enums;

namespace LakeshireAuth.Models.Requests;

public sealed class RegistrationRequest
{
    [Required(ErrorMessage = $"The {nameof(FirstName)} field is required.")]
    public string FirstName { get; set; } = "";
    
    [Required(ErrorMessage = $"The {nameof(LastName)} field is required.")]
    public string LastName { get; set; } = "";
    
    [Required(ErrorMessage = $"The {nameof(EmailAddress)} field is required.")]
    [EmailAddress(ErrorMessage = $"The {nameof(EmailAddress)} is invalid.")]
    public string EmailAddress { get; set; } = "";
    
    [Required(ErrorMessage = $"The {nameof(Password)} field is required.")]
    public string Password { get; set; } = "";
    
    [Required(ErrorMessage = $"The {nameof(Gender)} field is required.")]
    public EGender Gender { get; set; }
}