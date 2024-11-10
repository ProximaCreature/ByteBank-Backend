using AutoMapper;
using ByteBank.API.BillDiscount.Domain.Models.Aggregates;
using ByteBank.API.BillDiscount.Domain.Models.Commands;
using ByteBank.API.BillDiscount.Domain.Models.Responses;
using ByteBank.API.BillDiscount.Domain.Repositories;
using ByteBank.API.BillDiscount.Domain.Services;
using ByteBank.API.Shared.Domain.Repositories;

namespace ByteBank.API.BillDiscount.Application.Features;

public class BillCommandService : IBillCommandService
{
    private readonly IBillRepository _billRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BillCommandService(IBillRepository billRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _billRepository = billRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<BillResponse> Handle(CreateBillCommand command)
    {
        var billInDataBase = await _billRepository.GetBillByName(command.Name);

        if (billInDataBase != null)
        {
            throw new Exception($"Bill with name {command.Name} already exists.");
        } 
        
        var billEntity = _mapper.Map<Bill>(command);
        billEntity.IsDiscounted = false;
        await _billRepository.SaveAsync(billEntity);
        await _unitOfWork.CompleteAsync();
        
        var billResponse = _mapper.Map<BillResponse>(billEntity);
        return billResponse;
    }
}