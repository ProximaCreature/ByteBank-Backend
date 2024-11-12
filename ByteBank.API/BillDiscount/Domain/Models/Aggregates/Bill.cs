using ByteBank.API.BillDiscount.Domain.Models.ValueObjects;
using ByteBank.API.Shared.Domain.Models.Entities;
using ByteBank.API.Wallet.Domain.Models.Aggregates;

namespace ByteBank.API.BillDiscount.Domain.Models.Aggregates;

public class Bill : BaseDomainModel
{
    public required int WalletId { get; set; }
    public required string NombreCartera { get; set; }
    public required string Name { get; set; }
    
    public required double FaceValue { get; set; }
    public required Currency Currency { get; set; }
    public required DateTime ExpirationDate { get; set; }
    public required bool IsDiscounted { get; set; }
    
    public Wallets Wallets { get; set; }
}