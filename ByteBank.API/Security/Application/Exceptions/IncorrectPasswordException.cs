using ByteBank.API.Shared.Application.Exceptions;

namespace ByteBank.API.Security.Application.Exceptions;

public class IncorrectPasswordException : ValidationException
{
    public IncorrectPasswordException() : base("Incorrect password")
    {
    }
}