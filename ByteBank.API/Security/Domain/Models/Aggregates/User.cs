using ByteBank.API.Shared.Domain.Models.Entities;

namespace ByteBank.API.Security.Domain.Models.Aggregates;

public class User : BaseDomainModel
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}