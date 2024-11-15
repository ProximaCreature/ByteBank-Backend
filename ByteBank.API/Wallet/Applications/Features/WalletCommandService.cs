using AutoMapper;
using ByteBank.API.Security.Domain.Repositories;
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
    private readonly IUserRepository _userRepository;
    
    public WalletCommandService(IWalletRepository walletRepository, IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository)
    {
        _walletRepository = walletRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userRepository = userRepository;
    }
    
    public async Task<WalletResponse> Handle(CreateWalletCommand command)
    {
        var userInDatabase = await _userRepository.GetUserByUsernameAsync(command.Username);
        
        if (userInDatabase == null)
        {
            throw new Exception($"User with name not found.");
        }
        
        var walletsInDataBase = await _walletRepository.GetWalletByName(command.NombreCartera);
        
        if (walletsInDataBase != null)
        {
            throw new Exception($"Wallet with name {command.NombreCartera} already exists.");
        }
        
        var walletEntity = _mapper.Map<Wallets>(command);
        walletEntity.UserId = userInDatabase.Id;
        await _walletRepository.SaveAsync(walletEntity);
        await _unitOfWork.CompleteAsync();
        
        var walletResponse = _mapper.Map<WalletResponse>(walletEntity);
        return walletResponse;
    }
}