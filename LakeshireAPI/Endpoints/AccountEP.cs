using Microsoft.AspNetCore.Authorization;

namespace LakeshireAPI.Endpoints;

public static class AccountEP
{
    public static void AddTestEP(this WebApplication webApp)
    {
        webApp.MapGet("/test", [Authorize] () => Results.Ok())
            .WithName("Test")
            .ProducesValidationProblem(StatusCodes.Status401Unauthorized);
    }
}