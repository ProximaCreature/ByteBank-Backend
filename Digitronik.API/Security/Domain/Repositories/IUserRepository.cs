using Digitronik.API.Security.Domain.Models.Aggregates;
using Digitronik.API.Shared.Domain.Repositories;

namespace Digitronik.API.Security.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetUserByUsernameAsync(string username);
}