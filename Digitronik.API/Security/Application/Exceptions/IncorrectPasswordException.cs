using Digitronik.API.Shared.Application.Exceptions;

namespace Digitronik.API.Security.Application.Exceptions;

public class IncorrectPasswordException : ValidationException
{
    public IncorrectPasswordException() : base("Incorrect password")
    {
    }
}