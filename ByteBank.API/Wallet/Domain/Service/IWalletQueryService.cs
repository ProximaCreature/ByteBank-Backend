using ByteBank.API.Wallet.Domain.Models.Queries;
using ByteBank.API.Wallet.Domain.Models.Responses;

namespace ByteBank.API.Wallet.Domain.Service;

public interface IWalletQueryService
{
    Task<WalletResponse> Handle(GetWalletByIdQuery query);
    
    Task<WalletResponse> Handle(GetWalletByNameQuery query);
}