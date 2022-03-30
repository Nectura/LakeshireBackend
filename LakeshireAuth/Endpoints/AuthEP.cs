using Lakeshire.Common.DAL.WorkUnits;
using Lakeshire.Common.DAL.WorkUnits.Interfaces;
using LakeshireAuth.Models.Requests;
using LakeshireAuth.Models.Responses;
using LakeshireAuth.Services;
using LakeshireAuth.Validators;
using LakeshireAuth.Validators.Models;
using Microsoft.AspNetCore.Mvc;

namespace LakeshireAuth.Endpoints;

public static class AuthEP
{
    public static void AddLoginEP(this WebApplication webApp)
    {
        webApp.MapPost("/auth/login", async (
            [FromBody] LoginRequest request,
            [FromServices] IAuthWorkUnit authWorkUnit,
            [FromServices] PasswordValidator passwordValidator,
            [FromServices] JwtAuthService jwtAuthService,
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
                return Results.Unauthorized();

            var jwtToken = await jwtAuthService.CreateUserAccessTokenAsync(user, cancellationToken: cancellationToken);

            return Results.Ok(jwtToken);
        })
            .WithName("Login")
            .Produces<UserJwtToken>()
            .ProducesValidationProblem(StatusCodes.Status401Unauthorized);
    }
}