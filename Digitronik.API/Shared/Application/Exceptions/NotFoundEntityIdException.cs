namespace Digitronik.API.Shared.Application.Exceptions;

public class NotFoundEntityIdException : NotFoundEntityAttributeException
{
    public NotFoundEntityIdException(string entityName, object attributeValue) 
        : base(entityName, "Id", attributeValue)
    {
    }
}