using ByteBank.API.BillDiscount.Domain.Models.Aggregates;
using ByteBank.API.Shared.Domain.Repositories;

namespace ByteBank.API.BillDiscount.Domain.Repositories;

public interface IBillRepository : IBaseRepository<Bill>
{
    Task<Bill?> GetBillByName(string name);
    Task<IReadOnlyCollection<Bill>> GetNotDiscountedBills();
}