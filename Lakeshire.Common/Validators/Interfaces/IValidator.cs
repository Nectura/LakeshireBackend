namespace Lakeshire.Common.Validators.Interfaces;

public interface IValidator<in VRequest, VResponse>
    where VRequest : IValidationRequest
    where VResponse : IValidationResponse
{
    bool TryValidate(VRequest request, out VResponse? response);
}