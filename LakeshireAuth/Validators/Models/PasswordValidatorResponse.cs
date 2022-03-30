using Lakeshire.Common.Validators.Interfaces;

namespace LakeshireAuth.Validators.Models;

public class PasswordValidatorResponse : IAsyncValidatorResponse
{
    public bool IsValid { get; set; }
    public string? ValidationError { get; set; }
}