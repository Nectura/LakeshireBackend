using Lakeshire.Common.Validators.Interfaces;
using LakeshireAuth.Models.Requests;

namespace LakeshireAuth.Validators.Models;

public class RegistrationValidationRequest : IValidationRequest
{
    public RegistrationRequest RegistrationRequest { get; set; } = new ();
}