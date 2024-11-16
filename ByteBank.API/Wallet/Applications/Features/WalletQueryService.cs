using AutoMapper;
using ByteBank.API.BillDiscount.Domain.Models.Responses;
using ByteBank.API.Security.Domain.Models.Aggregates;
using ByteBank.API.Security.Domain.Repositories;
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
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper; 

    public WalletQueryService(IWalletRepository walletRepository, IMapper mapper, IUserRepository userRepository)
    {
        _walletRepository = walletRepository;
        _userRepository = userRepository;
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
                Tcea = wallet.pagoFueraDeFecha ? wallet.TCEAconMora() : wallet.TCEA(),
                ValorRecibido = wallet.CalcularValorRecibido(),
                ValorEntregado = wallet.pagoFueraDeFecha
                    ? wallet.CalcularValorEntregadoConMora()
                    : wallet.CalcularValorEntregado()
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
                Tcea = wallet.pagoFueraDeFecha ? wallet.TCEAconMora() : wallet.TCEA(),
                ValorRecibido = wallet.CalcularValorRecibido(),
                ValorEntregado = wallet.pagoFueraDeFecha
                    ? wallet.CalcularValorEntregadoConMora()
                    : wallet.CalcularValorEntregado(),
                Bills = wallet.Bills.Select(b => new BillResponse(
                    b.Id,
                    b.Name,
                    b.FaceValue,
                    b.Currency.ToString(),
                    b.ExpirationDate,
                    b.WalletId
                )).ToList()
            };
        }

        return walletResponse;
    }

    public async Task<List<WalletResponse>> Handle(GetAllWalletsByUserId query)
    {
        var userInDatabase = await _userRepository.FindByIdAsync(query.UserId);

        if (userInDatabase == null)
        {
            throw new NotFoundEntityIdException(nameof(User), query.UserId);
        }
        
        var wallets = await _walletRepository.GetAllWalletsByUserId(query.UserId);

        var walletsResponse = wallets
            .Select(_mapper.Map<WalletResponse>)
            .ToList();
        
        for (int i = 0; i < wallets.Count(); i++)
        {
            walletsResponse[i] = walletsResponse[i] with
            {
                HasAssociatedBill = true,
                Tcea = wallets[i].pagoFueraDeFecha? wallets[i].TCEAconMora() : wallets[i].TCEA(),
                ValorRecibido = wallets[i].CalcularValorRecibido(),
                ValorEntregado = wallets[i].pagoFueraDeFecha? wallets[i].CalcularValorEntregadoConMora() : wallets[i].CalcularValorEntregado() 
            };
        }

        return walletsResponse;
    }
}