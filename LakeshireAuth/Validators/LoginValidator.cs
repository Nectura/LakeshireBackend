using Lakeshire.Common.Services;
using Lakeshire.Common.Validators.Interfaces;
using LakeshireAuth.Validators.Models;

namespace LakeshireAuth.Validators;

public sealed class LoginValidator : IValidator<LoginValidationRequest, LoginValidationResponse>
{
    private readonly RegexValidationService _regexValidationService;

    public LoginValidator(RegexValidationService regexValidationService)
    {
        _regexValidationService = regexValidationService;
    }
    
    public bool TryValidate(LoginValidationRequest request, out LoginValidationResponse? response)
    {
        if (!ValidateEmailAddress(request.LoginRequest.EmailAddress))
        {
            response = new LoginValidationResponse
            {
                IsValid = false,
                ValidationError = "EmailAddress is invalid"
            };
            return false;
        }

        response = new LoginValidationResponse
        {
            IsValid = true
        };
        
        return true;
    }

    private bool ValidateEmailAddress(string input)
    {
        return _regexValidationService.TryValidateEmailAddress(input, out _);
    }
}