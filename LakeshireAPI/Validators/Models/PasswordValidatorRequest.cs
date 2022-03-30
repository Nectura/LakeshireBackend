using LakeshireAPI.Validators.Interfaces;

namespace LakeshireAPI.Validators.Models;

public class PasswordValidatorRequest : IAsyncValidatorRequest
{
    public string PasswordInput { get; set; } = "";
    public byte[] SaltHash { get; set; } = Array.Empty<byte>();
    public byte[] ExpectedOutput { get; set; } = Array.Empty<byte>();
}