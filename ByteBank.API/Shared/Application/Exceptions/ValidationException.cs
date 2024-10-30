namespace ByteBank.API.Shared.Application.Exceptions;

public abstract class ValidationException : Exception
{
    protected ValidationException(string message) : base(message)
    {
        
    }
}