using Digitronik.API.Shared.Application.Exceptions;

namespace Digitronik.API.Security.Application.Exceptions;

public class DuplicatedUserUsernameException : DuplicatedEntityAttributeException
{
    public DuplicatedUserUsernameException(object attributeValue) 
        : base("User", "Username", attributeValue)
    {
    }
}