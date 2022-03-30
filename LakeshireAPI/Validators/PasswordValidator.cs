using LakeshireAPI.Services.Interfaces;
using LakeshireAPI.Validators.Interfaces;
using LakeshireAPI.Validators.Models;

namespace LakeshireAPI.Validators;

public sealed class PasswordValidator : IAsyncValidator<PasswordValidatorRequest, PasswordValidatorResponse>
{
    private readonly IAuthService _authService;

    public PasswordValidator(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<PasswordValidatorResponse> ValidateAsync(PasswordValidatorRequest request, CancellationToken cancellationToken = default)
    {
        return new PasswordValidatorResponse
        {
            IsValid = await _authService.CompareHashesAsync(request.PasswordInput, request.SaltHash, request.ExpectedOutput, cancellationToken)
        };
    }
}