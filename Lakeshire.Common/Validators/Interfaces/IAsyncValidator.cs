namespace Lakeshire.Common.Validators.Interfaces;

public interface IAsyncValidator<in VRequest, VResponse>
    where VRequest : IAsyncValidatorRequest
    where VResponse : IAsyncValidatorResponse
{
    Task<VResponse> ValidateAsync(VRequest request, CancellationToken cancellationToken = default);
}