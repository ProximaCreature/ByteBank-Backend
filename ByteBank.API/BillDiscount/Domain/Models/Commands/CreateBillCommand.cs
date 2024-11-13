using System.ComponentModel.DataAnnotations;

namespace ByteBank.API.BillDiscount.Domain.Models.Commands;

public record CreateBillCommand(
    [Required] string NombreCartera,
    [Required] [MinLength(1, ErrorMessage = "Name cannot be empty")] string Name,
    [Required] double FaceValue,
    [Required] [MinLength(1, ErrorMessage = "Currency cannot be empty")] string Currency,
    [Required] DateTime ExpirationDate,
    [Required] int WalletId
);