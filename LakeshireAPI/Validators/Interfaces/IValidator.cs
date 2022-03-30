namespace LakeshireAPI.Validators.Interfaces;

public interface IValidator
{
    bool TryValidate(out string? validationError);
}