using ByteBank.API.BillDiscount.Domain.Models.ValueObjects;
using ByteBank.API.Shared.Domain.Models.Entities;

namespace ByteBank.API.BillDiscount.Domain.Models.Aggregates;

public class Bill : BaseDomainModel
{
    public required string Name { get; set; }
    public required double FaceValue { get; set; }
    public required Currency Currency { get; set; }
    public required DateTime ExpirationDate { get; set; }
}