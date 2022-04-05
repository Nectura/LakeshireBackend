using Lakeshire.Common.Validators.Interfaces;
using LakeshireAuth.Models.Requests;

namespace LakeshireAuth.Validators.Models;

public class RefreshTokenValidationRequest : IValidationRequest
{
    public RefreshTokenRequest RefreshTokenRequest { get; set; } = new ();
}