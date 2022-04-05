using Lakeshire.Common.Validators.Interfaces;

namespace LakeshireAuth.Validators.Models;

public class PasswordValidationRequest : IAsyncValidationRequest
{
    public string PasswordInput { get; set; } = "";
    public byte[] SaltHash { get; set; } = Array.Empty<byte>();
    public byte[] ExpectedOutput { get; set; } = Array.Empty<byte>();
}