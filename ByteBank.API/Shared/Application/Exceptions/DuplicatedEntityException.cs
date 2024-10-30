namespace ByteBank.API.Shared.Application.Exceptions;

public class DuplicatedEntityException : ConflictException
{
    public DuplicatedEntityException(string message) : base(message)
    {
    }
}