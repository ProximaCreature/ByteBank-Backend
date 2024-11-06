using AutoMapper;
using ByteBank.API.Security.Application.Exceptions;
using ByteBank.API.Security.Domain.Models.Aggregates;
using ByteBank.API.Security.Domain.Models.Commands;
using ByteBank.API.Security.Domain.Models.Responses;
using ByteBank.API.Security.Domain.Repositories;
using ByteBank.API.Security.Domain.Services;
using ByteBank.API.Shared.Application.Exceptions;
using ByteBank.API.Shared.Domain.Repositories;

namespace ByteBank.API.Security.Application.Features;

public class UserCommandService : IUserCommandService
{
    private readonly IUserRepository _userRepository;
    private readonly IEncryptService _encryptService;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UserCommandService(IUserRepository userRepository, IEncryptService encryptService, ITokenService tokenService, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _encryptService = encryptService;
        _tokenService = tokenService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<string> Handle(LoginUserCommand command)
    {
        var userInDatabase = await _userRepository.GetUserByUsernameAsync(command.Username);

        if (userInDatabase == null)
        {
            throw new NotFoundEntityAttributeException(nameof(User),nameof(command.Username), command.Username);
        }
        
        if (!_encryptService.Verify(command.Password, userInDatabase.Password))
        {
            throw new IncorrectPasswordException();
        }
        
        return _tokenService.GenerateToken(userInDatabase);
    }

    public async Task<UserResponse> Handle(RegisterUserCommand command)
    {
        var userInDatabase = await _userRepository.GetUserByUsernameAsync(command.Username);

        if (userInDatabase != null)
        {
            throw new DuplicatedUserUsernameException(command.Username);
        }
        
        var userEntity = _mapper.Map<User>(command);
        userEntity.Password = _encryptService.Encrypt(command.Password);

        await _userRepository.SaveAsync(userEntity);
        await _unitOfWork.CompleteAsync();
        
        var userResponse = _mapper.Map<UserResponse>(userEntity);
        return userResponse;

    }

    public async Task<UserResponse> Handle(int id, UpdateUserCommand command)
    {
        var userToUpdate = await _userRepository.FindByIdAsync(id);
        if (userToUpdate == null)
        {
            throw new NotFoundEntityIdException(nameof(User), id);
        }
        _mapper.Map(command, userToUpdate, typeof(UpdateUserCommand), typeof(User));
        userToUpdate.Password = _encryptService.Encrypt(command.Password);
        _userRepository.Modify(userToUpdate);
        await _unitOfWork.CompleteAsync();
        
        var userResponse = _mapper.Map<UserResponse>(userToUpdate);
        return userResponse;
    }

    public async Task<bool> Handle(DeleteUserCommand command)
    {
        var userToDelete = await _userRepository.FindByIdAsync(command.Id);
        if (userToDelete == null)
        {
            throw new NotFoundEntityIdException(nameof(User), command.Id);
        }
        _userRepository.Remove(userToDelete);
        await _unitOfWork.CompleteAsync();
        return true;
    }
}