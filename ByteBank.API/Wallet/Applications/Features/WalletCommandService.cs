using AutoMapper;
using ByteBank.API.Shared.Domain.Repositories;
using ByteBank.API.Wallet.Domain.Models.Aggregates;
using ByteBank.API.Wallet.Domain.Models.Commands;
using ByteBank.API.Wallet.Domain.Models.Responses;
using ByteBank.API.Wallet.Domain.Repository;
using ByteBank.API.Wallet.Domain.Service;

namespace ByteBank.API.Wallet.Applications.Features;

public class WalletCommandService : IWalletCommandService
{
    private readonly IWalletRepository _walletRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public WalletCommandService(IWalletRepository walletRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _walletRepository = walletRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<WalletResponse> Handle(CreateWalletCommand command)
    {
        var walletsInDataBase = await _walletRepository.GetWalletByName(command.NombreCartera);
        
        if (walletsInDataBase != null)
        {
            throw new Exception($"Wallet with name {command.NombreCartera} already exists.");
        }
        
        var walletEntity = _mapper.Map<Wallets>(command);
        await _walletRepository.SaveAsync(walletEntity);
        await _unitOfWork.CompleteAsync();
        
        var walletResponse = _mapper.Map<WalletResponse>(walletEntity);
        return walletResponse;
    }
}