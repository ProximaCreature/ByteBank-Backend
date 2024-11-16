namespace ByteBank.API.Security.Domain.Models.Responses;

public record LoginResponse(
    int UserId,
    string Username,
    string Token
);