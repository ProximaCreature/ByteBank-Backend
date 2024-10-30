using ByteBank.API.Security.Domain.Models.Aggregates;
using ByteBank.API.Shared.Domain.Models.Entities;

namespace ByteBank.API.Security.Domain.Models.Entities;

public class Role : BaseDomainModel
{
    public required string Name { get; set; }
    public required ICollection<User> Users { get; set; }
}