using Digitronik.API.Shared.Domain.Repositories;
using Digitronik.API.Shared.Infrastructure.Persistence.Configuration;

namespace Digitronik.API.Shared.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ServiceDbContext _context;

    public UnitOfWork(ServiceDbContext context)
    {
        _context = context;
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}