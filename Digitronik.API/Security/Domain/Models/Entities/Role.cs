using Digitronik.API.Security.Domain.Models.Aggregates;
using Digitronik.API.Shared.Domain.Models.Entities;

namespace Digitronik.API.Security.Domain.Models.Entities;

public class Role : BaseDomainModel
{
    public required string Name { get; set; }
    public required ICollection<User> Users { get; set; }
}