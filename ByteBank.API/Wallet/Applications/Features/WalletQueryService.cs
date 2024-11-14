using AutoMapper;
using ByteBank.API.BillDiscount.Domain.Models.Responses;
using ByteBank.API.Shared.Application.Exceptions;
using ByteBank.API.Wallet.Domain.Models.Aggregates;
using ByteBank.API.Wallet.Domain.Models.Queries;
using ByteBank.API.Wallet.Domain.Models.Responses;
using ByteBank.API.Wallet.Domain.Repository;
using ByteBank.API.Wallet.Domain.Service;

namespace ByteBank.API.Wallet.Applications.Features;

public class WalletQueryService : IWalletQueryService
{
    private readonly IWalletRepository _walletRepository;
    private readonly IMapper _mapper; 

    public WalletQueryService(IWalletRepository walletRepository, IMapper mapper)
    {
        _walletRepository = walletRepository;
        _mapper = mapper;
    }
    
    public async Task<WalletResponse> Handle(GetWalletByIdQuery query)
    {
        var wallet = await _walletRepository.FindByIdAsync(query.Id);
        if (wallet == null)
        {
            throw new NotFoundEntityIdException(nameof(Wallet), query.Id);
        }
        var walletResponse = _mapper.Map<WalletResponse>(wallet);
        if (wallet.Bills.Any())
        {
            walletResponse = walletResponse with
            {
                HasAssociatedBill = true,
                Tcea = wallet.pagoFueraDeFecha? wallet.TCEA() : wallet.TCEAconMora(),
                ValorRecibido = wallet.CalcularValorRecibido(),
                ValorEntregado = wallet.pagoFueraDeFecha? wallet.CalcularValorEntregadoConMora() : wallet.CalcularValorEntregado() 
            };
        }
        return walletResponse;
    }

    public async Task<WalletResponse> Handle(GetWalletByNameQuery query)
    {
        var wallet = await _walletRepository.GetWalletByName(query.Name);
        if (wallet == null)
        {
            throw new Exception("Wallet not found");
        }
        var walletResponse = _mapper.Map<WalletResponse>(wallet);
        if (wallet.Bills.Any())
        {
            walletResponse = walletResponse with
            {
                HasAssociatedBill = true,
                Tcea = wallet.pagoFueraDeFecha? wallet.TCEA() : wallet.TCEAconMora(),
                ValorRecibido = wallet.CalcularValorRecibido(),
                ValorEntregado = wallet.pagoFueraDeFecha? wallet.CalcularValorEntregadoConMora() : wallet.CalcularValorEntregado() 
            };
        }
        return walletResponse;
    }
}