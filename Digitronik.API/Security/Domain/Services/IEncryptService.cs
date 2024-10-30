namespace Digitronik.API.Security.Domain.Services;

public interface IEncryptService
{
    string Encrypt(string password);
    bool Verify(string password, string passwordHashed);
}