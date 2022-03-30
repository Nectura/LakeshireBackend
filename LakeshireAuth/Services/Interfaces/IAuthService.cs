namespace LakeshireAuth.Services.Interfaces;

public interface IAuthService
{
    Task<byte[]> ComputeHashAsync(string input, CancellationToken cancellationToken = default);
    Task<byte[]> ComputeHashAsync(byte[] input, CancellationToken cancellationToken = default);
    Task<(byte[] SaltHash, byte[] FinalizedOutput)> EncryptInputAsync(string input, CancellationToken cancellationToken = default);
    Task<bool> CompareHashesAsync(string input, byte[] saltHash, byte[] expectedOutput, CancellationToken cancellationToken = default);
    
    string GenerateRandomCryptoSafeString();
}