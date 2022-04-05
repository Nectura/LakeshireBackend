using LakeshireAuth.Services;
using LakeshireAuth.Validators;
using LakeshireAuth.Validators.Models;

namespace LakeshireTests;

public class AuthUnitTest
{
    [Fact]
    public async Task AssertThatInputEncryptionReturnsValidResults()
    {
        const string passwordInput = "abcdef";

        var authService = new AuthService();

        var (saltHash, finalizedOutput) = await authService.EncryptInputAsync(passwordInput);

        Assert.True(saltHash.Length > 0 && finalizedOutput.Length > 0, "The salt has or the finalized output hash are empty!");
    }

    [Fact]
    public async Task AssertThatEncryptedInputCanBeCheckedAgainstQueries()
    {
        const string passwordInput = "abcdef";
        const string wrongPasswordInput = "abcdeee";

        var authService = new AuthService();

        var (saltHash, finalizedOutput) = await authService.EncryptInputAsync(passwordInput);

        var passwordValidator = new PasswordValidator(authService);

        var correctPasswordValidationResponse = await passwordValidator.ValidateAsync(new PasswordValidationRequest
        {
            PasswordInput = passwordInput,
            SaltHash = saltHash,
            ExpectedOutput = finalizedOutput
        });

        Assert.True(correctPasswordValidationResponse.IsValid, $"Failed to validate the user password after encryption: {correctPasswordValidationResponse.ValidationError ?? string.Empty}");

        var wrongPasswordValidationResponse = await passwordValidator.ValidateAsync(new PasswordValidationRequest
        {
            PasswordInput = wrongPasswordInput,
            SaltHash = saltHash,
            ExpectedOutput = finalizedOutput
        });

        Assert.False(wrongPasswordValidationResponse.IsValid, $"Failed to validate the user password after encryption: {wrongPasswordValidationResponse.ValidationError ?? string.Empty}");
    }
}