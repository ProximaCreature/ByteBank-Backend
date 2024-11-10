namespace ByteBank.API.BillDiscount.Domain.Models.Responses;

public record BillResponse(
    int Id,
    string Name,
    double FaceValue,
    string Currency,
    DateTime ExpirationDate
);