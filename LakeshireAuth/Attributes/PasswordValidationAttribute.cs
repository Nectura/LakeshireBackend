using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LakeshireAuth.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class PasswordValidationAttribute : ValidationAttribute
{
    private readonly bool _shouldHaveAtLeastOneCapitalLetter;
    private readonly bool _shouldHaveAtLeastOneDigit;
    private readonly bool _shouldHaveAtLeastOneNonAlphaNumeric;
    private readonly int _minimumLength;

    public PasswordValidationAttribute(
        bool shouldHaveAtLeastOneCapitalLetter = true,
        bool shouldHaveAtLeastOneDigit = true,
        bool shouldHaveAtLeastOneNonAlphaNumeric = true,
        int minimumLength = 8)
    {
        _shouldHaveAtLeastOneCapitalLetter = shouldHaveAtLeastOneCapitalLetter;
        _shouldHaveAtLeastOneDigit = shouldHaveAtLeastOneDigit;
        _shouldHaveAtLeastOneNonAlphaNumeric = shouldHaveAtLeastOneNonAlphaNumeric;
        _minimumLength = minimumLength;
    }

    public override bool IsValid(object? value)
    {
        if (value is not string strVal) return false;

        if (!HasTheMinimumAmountOfCharacters(strVal))
            throw new ArgumentException($"The password needs to consist of at least {_minimumLength:n0} {(_minimumLength == 1 ? "character" : "characters")}.");

        if (_shouldHaveAtLeastOneDigit && !HasAtLeastOneDigit(strVal))
            throw new ArgumentException("The password needs to have at least 1 digit.");

        if (_shouldHaveAtLeastOneCapitalLetter && !HasAtLeastOneCapitalLetter(strVal))
            throw new ArgumentException("The password needs to have at least 1 capital letter.");

        if (_shouldHaveAtLeastOneNonAlphaNumeric && !HasAtLeastOneNonAlphaNumericCharacter(strVal))
            throw new ArgumentException("The password needs to have at least 1 non alpha-numeric character.");

        return true;
    }

    private static bool HasAtLeastOneCapitalLetter(string password)
    {
        return Regex.IsMatch(password, "(?=.*[A-Z])");
    }

    private static bool HasAtLeastOneDigit(string password)
    {
        return Regex.IsMatch(password, "(?=.*\\d)");
    }

    private static bool HasAtLeastOneNonAlphaNumericCharacter(string password)
    {
        return Regex.IsMatch(password, "(?=.*[-+_!@#$%^&*.,?])");
    }

    private bool HasTheMinimumAmountOfCharacters(string password)
    {
        return password.Length >= _minimumLength;
    }
}