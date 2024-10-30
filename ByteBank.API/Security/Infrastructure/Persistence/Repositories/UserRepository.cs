using ByteBank.API.Security.Domain.Models.Aggregates;
using ByteBank.API.Security.Domain.Repositories;
using ByteBank.API.Shared.Infrastructure.Persistence.Configuration;
using ByteBank.API.Shared.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ByteBank.API.Security.Infrastructure.Persistence.Repositories;

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