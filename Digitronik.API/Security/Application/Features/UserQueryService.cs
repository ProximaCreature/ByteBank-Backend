using AutoMapper;
using Digitronik.API.Security.Domain.Models.Queries;
using Digitronik.API.Security.Domain.Models.Responses;
using Digitronik.API.Security.Domain.Repositories;
using Digitronik.API.Security.Domain.Services;

namespace Digitronik.API.Security.Application.Features;

public class UserQueryService : IUserQueryService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserQueryService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public Task<IReadOnlyCollection<UserResponse>> Handle(GetUsersQuery query)
    {
        throw new NotImplementedException();
    }

    public Task<UserResponse?> Handle(GetUserByIdQuery query)
    {
        throw new NotImplementedException();
    }
}