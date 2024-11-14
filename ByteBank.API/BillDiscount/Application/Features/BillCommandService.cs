using AutoMapper;
using ByteBank.API.BillDiscount.Domain.Models.Aggregates;
using ByteBank.API.BillDiscount.Domain.Models.Commands;
using ByteBank.API.BillDiscount.Domain.Models.Responses;
using ByteBank.API.BillDiscount.Domain.Repositories;
using ByteBank.API.BillDiscount.Domain.Services;
using ByteBank.API.Shared.Application.Exceptions;
using ByteBank.API.Shared.Domain.Repositories;
using ByteBank.API.Wallet.Domain.Models.Aggregates;
using ByteBank.API.Wallet.Domain.Repository;

namespace ByteBank.API.BillDiscount.Application.Features;

public class BillCommandService : IBillCommandService
{
    private readonly IBillRepository _billRepository;
    private readonly IWalletRepository _walletRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BillCommandService(IBillRepository billRepository, IUnitOfWork unitOfWork, IMapper mapper, IWalletRepository walletRepository)
    {
        _billRepository = billRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _walletRepository = walletRepository;
    }
    
    public async Task<BillResponse> Handle(CreateBillCommand command)
    {
        var wallet = await _walletRepository.GetWalletByName(command.NombreCartera);
        if (wallet == null)
        {
            throw new Exception($"Wallet with name not found.");
        }
        
        var billInDataBase = await _billRepository.GetBillByName(command.Name);
        Wallets? walletInDatabase = await _walletRepository.FindByIdAsync(command.WalletId);

        if (walletInDatabase == null)
        {
            throw new NotFoundEntityIdException(nameof(Wallets), command.WalletId);
        }

        if (billInDataBase != null)
        {
            throw new Exception($"Bill with name {command.Name} already exists.");
        } 
        
        var billEntity = _mapper.Map<Bill>(command);
        billEntity.WalletId = wallet.Id;
        billEntity.IsDiscounted = false;
        await _billRepository.SaveAsync(billEntity);
        await _unitOfWork.CompleteAsync();
        
        var billResponse = _mapper.Map<BillResponse>(billEntity);
        return billResponse;
    }
}