using Lakeshire.Common.Validators.Interfaces;
using LakeshireAuth.Services.Interfaces;
using LakeshireAuth.Validators.Models;

namespace LakeshireAuth.Validators;

public sealed class PasswordValidator : IAsyncValidator<PasswordValidationRequest, PasswordValidationResponse>
{
    private readonly IAuthService _authService;

    public PasswordValidator(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<PasswordValidationResponse> ValidateAsync(PasswordValidationRequest request, CancellationToken cancellationToken = default)
    {
        return new PasswordValidationResponse
        {
            IsValid = await _authService.CompareHashesAsync(request.PasswordInput, request.SaltHash, request.ExpectedOutput, cancellationToken)
        };
    }
}