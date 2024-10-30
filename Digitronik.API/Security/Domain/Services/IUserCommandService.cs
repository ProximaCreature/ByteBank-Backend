using Digitronik.API.Security.Domain.Models.Commands;
using Digitronik.API.Security.Domain.Models.Responses;

namespace Digitronik.API.Security.Domain.Services;

public interface IUserCommandService
{
    Task<string> Handle(LoginUserCommand command);
    Task<UserResponse> Handle(RegisterUserCommand command);
    Task<UserResponse> Handle(int id, UpdateUserCommand command);
    Task<bool> Handle(DeleteUserCommand command);
}