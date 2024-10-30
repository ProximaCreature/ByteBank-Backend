using ByteBank.API.Shared.Domain.Repositories;
using ByteBank.API.Shared.Infrastructure.Persistence.Configuration;

namespace ByteBank.API.Shared.Infrastructure.Persistence.Repositories;

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