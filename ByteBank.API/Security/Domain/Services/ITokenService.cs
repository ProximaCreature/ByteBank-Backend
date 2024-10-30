using ByteBank.API.Security.Domain.Models.Aggregates;

namespace ByteBank.API.Security.Domain.Services;

public interface ITokenService
{
    string GenerateToken(User user);
    Task<int?> ValidateToken(string token);
}