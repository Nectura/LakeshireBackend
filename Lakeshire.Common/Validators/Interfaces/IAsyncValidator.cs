namespace Lakeshire.Common.Validators.Interfaces;

public interface IAsyncValidator<in VRequest, VResponse>
    where VRequest : IAsyncValidationRequest
    where VResponse : IAsyncValidationResponse
{
    Task<VResponse> ValidateAsync(VRequest request, CancellationToken cancellationToken = default);
}