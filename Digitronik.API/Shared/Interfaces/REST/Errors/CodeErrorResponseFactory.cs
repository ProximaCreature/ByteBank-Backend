using System.Net;
using Digitronik.API.Shared.Application.Exceptions;

namespace Digitronik.API.Shared.Interfaces.REST.Errors;

public static class CodeErrorResponseFactory
{
    public static CodeErrorResponse CreateCodeErrorResponse(Exception exception)
    {
        var statusCode = exception switch
        {
            NotFoundException => (int)HttpStatusCode.NotFound,
            ValidationException => (int)HttpStatusCode.BadRequest,
            ConflictException => (int)HttpStatusCode.Conflict,
            _ => (int)HttpStatusCode.InternalServerError
        };

        return new CodeErrorResponse(statusCode, exception.Message);
    }
}