namespace ByteBank.API.Shared.Domain.Models.Entities;

public abstract class BaseDomainModel
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}