using ByteBank.API.Security.Domain.Models.Queries;
using ByteBank.API.Security.Domain.Models.Responses;

namespace ByteBank.API.Security.Domain.Services;

public interface IUserQueryService
{
    Task<IReadOnlyCollection<UserResponse>> Handle(GetUsersQuery query);
    Task<UserResponse?> Handle(GetUserByIdQuery query);
}