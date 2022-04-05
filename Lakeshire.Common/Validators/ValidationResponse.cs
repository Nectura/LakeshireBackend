using Lakeshire.Common.Validators.Interfaces;

namespace Lakeshire.Common.Validators;

public abstract class ValidationResponse : IValidationResponse
{
    public virtual bool IsValid { get; set; }
    public virtual string? ValidationError { get; set; }
}