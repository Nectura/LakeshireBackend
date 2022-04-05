using Lakeshire.Common.Validators.Interfaces;
using LakeshireAuth.Validators.Models;

namespace LakeshireAuth.Validators;

public sealed class RefreshTokenValidator : IValidator<RefreshTokenValidationRequest, RefreshTokenValidationResponse>
{
    public bool TryValidate(RefreshTokenValidationRequest request, out RefreshTokenValidationResponse? response)
    {
        if (!Guid.TryParse(request.RefreshTokenRequest.UserId, out _))
        {
            response = new RefreshTokenValidationResponse
            {
                IsValid = false,
                ValidationError = "UserId is invalid"
            };
            return false;
        }
        
        response = new RefreshTokenValidationResponse
        {
            IsValid = true
        };
        
        return true;
    }
}