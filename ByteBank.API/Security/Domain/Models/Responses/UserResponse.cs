namespace ByteBank.API.Security.Domain.Models.Responses;

public record UserResponse(
    int Id,
    string UserName,
    string Email,
    string FirstName,
    string LastName
);