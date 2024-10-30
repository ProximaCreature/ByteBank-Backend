using ByteBank.API.Shared.Application.Exceptions;

namespace ByteBank.API.Security.Application.Exceptions;

public class DuplicatedUserUsernameException : DuplicatedEntityAttributeException
{
    public DuplicatedUserUsernameException(object attributeValue) 
        : base("User", "Username", attributeValue)
    {
    }
}