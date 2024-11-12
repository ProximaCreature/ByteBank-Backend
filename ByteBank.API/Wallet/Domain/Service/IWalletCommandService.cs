using ByteBank.API.Wallet.Domain.Models.Aggregates;
using ByteBank.API.Wallet.Domain.Models.Commands;
using ByteBank.API.Wallet.Domain.Models.Responses;

namespace ByteBank.API.Wallet.Domain.Service;

public interface IWalletCommandService
{
    Task<WalletResponse> Handle(CreateWalletCommand command);
}