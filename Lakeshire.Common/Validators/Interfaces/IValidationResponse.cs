namespace Lakeshire.Common.Validators.Interfaces;

public interface IValidationResponse
{
    public bool IsValid { get; set; }
    public string? ValidationError { get; set; }
}