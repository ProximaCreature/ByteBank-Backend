using AutoMapper;
using ByteBank.API.Wallet.Domain.Models.Aggregates;
using ByteBank.API.Wallet.Domain.Models.Responses;

namespace ByteBank.API.Wallet.Applications.Mappings;

public class ModelToResponse : Profile
{
    public ModelToResponse()
    {
        CreateMap<Wallets, WalletResponse>();
    }
}