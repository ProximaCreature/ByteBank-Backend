using ByteBank.API.Shared.Domain.Repositories;
using ByteBank.API.Shared.Infrastructure.Persistence.Configuration;
using ByteBank.API.Shared.Infrastructure.Persistence.Repositories;
using ByteBank.API.Wallet.Domain.Models.Aggregates;
using ByteBank.API.Wallet.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace ByteBank.API.Wallet.Infrastructures.Persistence.Repositories;

public class WalletRepository : BaseRepository<Wallets>, IWalletRepository
{
    public WalletRepository(ServiceDbContext context) : base(context)
    {
    }
    
    public async Task<Wallets> GetWalletByName(string name)
    {
        return await Context.Wallets
            .Where(w => w.NombreCartera == name)
            .FirstOrDefaultAsync();
    }
}