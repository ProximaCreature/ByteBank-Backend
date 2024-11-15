using AutoMapper;
using ByteBank.API.Wallet.Domain.Models.Aggregates;
using ByteBank.API.Wallet.Domain.Models.Commands;

namespace ByteBank.API.Wallet.Applications.Mappings;

public class RequestToModel : Profile
{
    public RequestToModel()
    {
        CreateMap<CreateWalletCommand, Wallets>();
    }
}
