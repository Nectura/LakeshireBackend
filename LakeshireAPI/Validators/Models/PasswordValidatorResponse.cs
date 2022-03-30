using LakeshireAPI.Validators.Interfaces;

namespace LakeshireAPI.Validators.Models;

public class PasswordValidatorResponse : IAsyncValidatorResponse
{
    public bool IsValid { get; set; }
    public string? ValidationError { get; set; }
}