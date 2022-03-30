namespace LakeshireAPI.Validators.Interfaces;

public interface IAsyncValidatorResponse
{
    public bool IsValid { get; set; }
    public string? ValidationError { get; set; }
}