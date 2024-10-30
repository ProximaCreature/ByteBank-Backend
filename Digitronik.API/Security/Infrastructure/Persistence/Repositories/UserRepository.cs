using Digitronik.API.Security.Domain.Models.Aggregates;
using Digitronik.API.Security.Domain.Repositories;
using Digitronik.API.Shared.Infrastructure.Persistence.Configuration;
using Digitronik.API.Shared.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Digitronik.API.Security.Infrastructure.Persistence.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ServiceDbContext context) : base(context)
    {
    }
    
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await Context.Users
            .Where(user => user.Username == username)
            .FirstOrDefaultAsync();
    }
}