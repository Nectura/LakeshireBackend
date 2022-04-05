using Lakeshire.Common.Validators.Interfaces;
using LakeshireAuth.Models.Requests;

namespace LakeshireAuth.Validators.Models;

public class LoginValidationRequest : IValidationRequest
{
    public LoginRequest LoginRequest { get; set; } = new ();
}