using AutoMapper;
using ByteBank.API.Security.Domain.Models.Aggregates;
using ByteBank.API.Security.Domain.Models.Queries;
using ByteBank.API.Security.Domain.Models.Responses;
using ByteBank.API.Security.Domain.Repositories;
using ByteBank.API.Security.Domain.Services;
using ByteBank.API.Shared.Application.Exceptions;

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

    public async Task<UserResponse?> Handle(GetUserByIdQuery query)
    {
        var user = await _userRepository.FindByIdAsync(query.Id);
        if (user == null)
        {
            throw new NotFoundEntityIdException(nameof(User), query.Id);
        }
        var userResponse = _mapper.Map<UserResponse>(user);
        return userResponse;
    }
}