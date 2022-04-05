using Lakeshire.Common.Enums;
using Lakeshire.Common.Services;
using Lakeshire.Common.Validators.Interfaces;
using LakeshireAuth.Validators.Models;

namespace LakeshireAuth.Validators;

public sealed class RegistrationValidator : IValidator<RegistrationValidationRequest, RegistrationValidationResponse>
{    
    private readonly RegexValidationService _regexValidationService;

    public RegistrationValidator(RegexValidationService regexValidationService)
    {
        _regexValidationService = regexValidationService;
    }
    
    public bool TryValidate(RegistrationValidationRequest request, out RegistrationValidationResponse? response)
    {
        if (!ValidateEmailAddress(request.RegistrationRequest.EmailAddress))
        {
            response = new RegistrationValidationResponse
            {
                IsValid = false,
                ValidationError = "EmailAddress is invalid"
            };
            return false;
        }

        if (!ValidateGender(request.RegistrationRequest.Gender))
        {
            response = new RegistrationValidationResponse
            {
                IsValid = false,
                ValidationError = "Gender is invalid"
            };
            return false;
        }

        response = new RegistrationValidationResponse
        {
            IsValid = true
        };
        
        return true;
    }
    
    private bool ValidateEmailAddress(string input)
    {
        return _regexValidationService.TryValidateEmailAddress(input, out _);
    }

    private bool ValidateGender(int gender)
    {
        return Enum.IsDefined(typeof(EGender), gender);
    }
}