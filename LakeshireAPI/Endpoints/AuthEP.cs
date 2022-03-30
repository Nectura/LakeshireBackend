using Lakeshire.Common.DTO;
using LakeshireAPI.DAL.WorkUnits;
using LakeshireAPI.Validators;
using LakeshireAPI.Validators.Models;
using Microsoft.AspNetCore.Mvc;

namespace LakeshireAPI.Endpoints;

public static class AuthEP
{
    public static void AddLoginEP(this WebApplication webApp)
    {
        webApp.MapPost("/auth/login", async (
            [FromBody] LoginRequestDTO request,
            [FromServices] AuthWorkUnit authWorkUnit,
            [FromServices] PasswordValidator passwordValidator,
            CancellationToken cancellationToken) =>
        {
            var user = await authWorkUnit.UserAccounts.FindAsync(request.EmailAddress, cancellationToken);

            if (user == default)
                return Results.Unauthorized();

            var passwordValidationRequest = new PasswordValidatorRequest
            {
                PasswordInput = request.Password,
                SaltHash = user.SaltHash,
                ExpectedOutput = user.PasswordHash
            };
            
            var passwordValidationResponse = await passwordValidator.ValidateAsync(passwordValidationRequest, cancellationToken);

            if (!passwordValidationResponse.IsValid)
            {
                Console.WriteLine($"Password validation failed because of: {passwordValidationResponse.ValidationError}");
                return Results.Unauthorized();
            }
            
            // TODO: create JWT token here and return to the client
            
            return Results.Ok();
        }).WithName("Login");
    }
}