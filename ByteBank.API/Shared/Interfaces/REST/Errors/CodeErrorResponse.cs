namespace ByteBank.API.Shared.Interfaces.REST.Errors;

public class CodeErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public CodeErrorResponse(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}