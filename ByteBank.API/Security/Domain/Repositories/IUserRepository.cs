using ByteBank.API.Security.Domain.Models.Aggregates;
using ByteBank.API.Shared.Domain.Repositories;

namespace ByteBank.API.Security.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetUserByUsernameAsync(string username);
}