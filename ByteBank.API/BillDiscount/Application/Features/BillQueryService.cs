using AutoMapper;
using ByteBank.API.BillDiscount.Domain.Models.Aggregates;
using ByteBank.API.BillDiscount.Domain.Models.Queries;
using ByteBank.API.BillDiscount.Domain.Models.Responses;
using ByteBank.API.BillDiscount.Domain.Repositories;
using ByteBank.API.BillDiscount.Domain.Services;
using ByteBank.API.Shared.Application.Exceptions;

namespace ByteBank.API.BillDiscount.Application.Features;

public class BillQueryService : IBillQueryService
{
    private readonly IBillRepository _billRepository;
    private readonly IMapper _mapper;
    
    public BillQueryService(IBillRepository billRepository, IMapper mapper)
    {
        _billRepository = billRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<BillResponse>> Handle(GetAllBillsQuery query)
    {
        var bills = await _billRepository.GetNotDiscountedBills();
        var billsResponse = bills
            .Select(_mapper.Map<BillResponse>)
            .ToList()
            .AsReadOnly();
        return billsResponse;
    }

    public async Task<BillResponse> Handle(GetBillByIdQuery query)
    {
        var bill = await _billRepository.FindByIdAsync(query.Id);
        if (bill == null)
        {
            throw new NotFoundEntityIdException(nameof(Bill), query.Id);
        }
        var billResponse = _mapper.Map<BillResponse>(bill);
        return billResponse;
    }
}