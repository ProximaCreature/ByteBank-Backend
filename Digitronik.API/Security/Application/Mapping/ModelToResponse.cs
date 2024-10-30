using AutoMapper;
using Digitronik.API.Security.Domain.Models.Aggregates;
using Digitronik.API.Security.Domain.Models.Responses;

namespace Digitronik.API.Security.Application.Mapping;

public class ModelToResponse : Profile
{
    public ModelToResponse()
    {
        CreateMap<User, UserResponse>();
    }
}