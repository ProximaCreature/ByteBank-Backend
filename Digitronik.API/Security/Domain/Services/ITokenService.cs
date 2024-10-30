using Digitronik.API.Security.Domain.Models.Aggregates;

namespace Digitronik.API.Security.Domain.Services;

public interface ITokenService
{
    string GenerateToken(User user);
    Task<int?> ValidateToken(string token);
}