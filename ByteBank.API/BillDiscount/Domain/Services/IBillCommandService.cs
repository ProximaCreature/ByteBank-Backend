using System.Reflection.Metadata;
using ByteBank.API.BillDiscount.Domain.Models.Commands;
using ByteBank.API.BillDiscount.Domain.Models.Responses;

namespace ByteBank.API.BillDiscount.Domain.Services;

public interface IBillCommandService
{
    Task<BillResponse> Handle(CreateBillCommand command);
}