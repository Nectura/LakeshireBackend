using Lakeshire.Common.DAL.Models;
using Lakeshire.Common.DAL.WorkUnits.Interfaces;
using Lakeshire.Common.Enums;
using LakeshireAuth.Models.Requests;
using LakeshireAuth.Models.Responses;
using LakeshireAuth.Services;
using LakeshireAuth.Services.Interfaces;
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
                [FromServices] LoginValidator loginValidator,
                [FromServices] IAuthWorkUnit authWorkUnit,
                [FromServices] PasswordValidator passwordValidator,
                [FromServices] JwtAuthService jwtAuthService,
                CancellationToken cancellationToken) =>
            {
                var loginValidationRequest = new LoginValidationRequest
                {
                    LoginRequest = request
                };

                if (!loginValidator.TryValidate(loginValidationRequest, out var validationResponse))
                    return Results.BadRequest(validationResponse);

                var user = await authWorkUnit.UserAccounts.FindByEmailAddressAsync(request.EmailAddress, cancellationToken);

                if (user == default)
                    return Results.Unauthorized();

                var passwordValidationRequest = new PasswordValidationRequest
                {
                    PasswordInput = request.Password,
                    SaltHash = user.SaltHash,
                    ExpectedOutput = user.PasswordHash
                };

                var passwordValidationResponse =
                    await passwordValidator.ValidateAsync(passwordValidationRequest, cancellationToken);

                if (!passwordValidationResponse.IsValid)
                    return Results.Unauthorized();

                var jwtToken =
                    await jwtAuthService.CreateUserAccessTokenAsync(user, cancellationToken: cancellationToken);

                return Results.Ok(jwtToken);
            })
            .WithName("Login")
            .Produces<UserJwtTokenResponse>()
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem(StatusCodes.Status401Unauthorized);
    }

    public static void AddRegisterEP(this WebApplication webApp)
    {
        webApp.MapPost("/auth/register", async (
                [FromBody] RegistrationRequest request,
                [FromServices] RegistrationValidator registrationValidator,
                [FromServices] IAuthWorkUnit authWorkUnit,
                [FromServices] PasswordStructureValidator passwordStructureValidator,
                [FromServices] IAuthService authService,
                CancellationToken cancellationToken) =>
            {
                var registrationValidationRequest = new RegistrationValidationRequest
                {
                    RegistrationRequest = request
                };

                if (!registrationValidator.TryValidate(registrationValidationRequest, out var validationResponse))
                    return Results.BadRequest(validationResponse);
                
                if (!passwordStructureValidator.Validate(request.Password, out var validationError))
                    return Results.BadRequest(validationError);
                
                if (await authWorkUnit.UserAccounts.IsEmailTakenAsync(request.EmailAddress, cancellationToken))
                    return Results.Conflict();

                var (saltHash, finalizedOutput) =
                    await authService.EncryptInputAsync(request.Password, cancellationToken);

                authWorkUnit.UserAccounts.Add(new UserAccount
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    EmailAddress = request.EmailAddress,
                    Gender = (EGender) request.Gender,
                    SaltHash = saltHash,
                    PasswordHash = finalizedOutput
                });

                await authWorkUnit.CommitChangesAsync(cancellationToken);

                return Results.Ok();
            })
            .WithName("Register")
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem(StatusCodes.Status409Conflict);
    }

    public static void AddRefreshTokenEP(this WebApplication webApp)
    {
        webApp.MapPost("/auth/refresh", async (
                [FromBody] RefreshTokenRequest request,
                [FromServices] RefreshTokenValidator refreshTokenValidator,
                [FromServices] JwtAuthService jwtAuthService,
                CancellationToken cancellationToken) =>
            {
                var refreshTokenValidationRequest = new RefreshTokenValidationRequest
                {
                    RefreshTokenRequest = request
                };

                if (!refreshTokenValidator.TryValidate(refreshTokenValidationRequest, out var validationResponse))
                    return Results.BadRequest(validationResponse);

                var refreshToken = await jwtAuthService.RefreshUserAccessTokenAsync(Guid.Parse(request.UserId),
                    request.RefreshToken,
                    cancellationToken: cancellationToken);

                return refreshToken == default
                    ? Results.Unauthorized()
                    : Results.Ok(refreshToken);
            })
            .WithName("Refresh")
            .Produces<UserJwtTokenResponse>()
            .ProducesValidationProblem(StatusCodes.Status401Unauthorized);
    }
}