namespace ByteBank.API.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}