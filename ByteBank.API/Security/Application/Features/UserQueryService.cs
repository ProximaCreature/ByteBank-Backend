using AutoMapper;
using ByteBank.API.Security.Domain.Models.Queries;
using ByteBank.API.Security.Domain.Models.Responses;
using ByteBank.API.Security.Domain.Repositories;
using ByteBank.API.Security.Domain.Services;

namespace ByteBank.API.Security.Application.Features;

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