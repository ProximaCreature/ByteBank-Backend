using Digitronik.API.Security.Domain.Models.Queries;
using Digitronik.API.Security.Domain.Models.Responses;

namespace Digitronik.API.Security.Domain.Services;

public interface IUserQueryService
{
    Task<IReadOnlyCollection<UserResponse>> Handle(GetUsersQuery query);
    Task<UserResponse?> Handle(GetUserByIdQuery query);
}