using AutoMapper;
using ByteBank.API.Security.Domain.Models.Aggregates;
using ByteBank.API.Security.Domain.Models.Commands;

namespace ByteBank.API.Security.Application.Mapping;

public class RequestToModel : Profile
{
    public RequestToModel()
    {
        CreateMap<RegisterUserCommand, User>();
        CreateMap<UpdateUserCommand, User>();
    }
}