using ByteBank.API.Security.Domain.Models.Commands;
using ByteBank.API.Security.Domain.Models.Responses;

namespace ByteBank.API.Security.Domain.Services;

public interface IUserCommandService
{
    Task<LoginResponse> Handle(LoginUserCommand command);
    Task<UserResponse> Handle(RegisterUserCommand command);
    Task<UserResponse> Handle(int id, UpdateUserCommand command);
    Task<bool> Handle(DeleteUserCommand command);
}