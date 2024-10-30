using Digitronik.API.Shared.Domain.Models.Entities;

namespace Digitronik.API.Security.Domain.Models.Aggregates;

public class User : BaseDomainModel
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}