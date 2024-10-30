using AutoMapper;
using ByteBank.API.Security.Domain.Models.Aggregates;
using ByteBank.API.Security.Domain.Models.Responses;

namespace ByteBank.API.Security.Application.Mapping;

public class ModelToResponse : Profile
{
    public ModelToResponse()
    {
        CreateMap<User, UserResponse>();
    }
}