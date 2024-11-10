using ByteBank.API.BillDiscount.Domain.Models.Aggregates;
using ByteBank.API.BillDiscount.Domain.Repositories;
using ByteBank.API.Shared.Infrastructure.Persistence.Configuration;
using ByteBank.API.Shared.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ByteBank.API.BillDiscount.Infrastructure.Persistence.Repositories;

public class BillRepository : BaseRepository<Bill>, IBillRepository
{
    public BillRepository(ServiceDbContext context) : base(context)
    {
    }

    public async Task<Bill?> GetBillByName(string name)
    {
        return await Context.Bills
            .Where(b => b.Name == name)
            .FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<Bill>> GetNotDiscountedBills()
    {
        return await Context.Bills
            .Where(b => b.IsDiscounted == false)
            .ToListAsync();
    }
}