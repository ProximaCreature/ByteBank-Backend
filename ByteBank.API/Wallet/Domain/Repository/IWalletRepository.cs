using ByteBank.API.Shared.Domain.Repositories;
using ByteBank.API.Wallet.Domain.Models.Aggregates;

namespace ByteBank.API.Wallet.Domain.Repository;

public interface IWalletRepository : IBaseRepository<Wallets>
{
    Task<Wallets?> GetWalletByName(string name);
    Task<List<Wallets>> GetAllWalletsByUserId(int userId);
}