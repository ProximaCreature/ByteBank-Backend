using ByteBank.API.BillDiscount.Domain.Models.Queries;
using ByteBank.API.BillDiscount.Domain.Models.Responses;

namespace ByteBank.API.BillDiscount.Domain.Services;

public interface IBillQueryService
{
    Task<IReadOnlyCollection<BillResponse>> Handle(GetAllBillsQuery query);
    Task<BillResponse> Handle(GetBillByIdQuery query);
}