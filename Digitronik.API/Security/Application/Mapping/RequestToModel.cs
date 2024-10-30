using AutoMapper;
using Digitronik.API.Security.Domain.Models.Aggregates;
using Digitronik.API.Security.Domain.Models.Commands;

namespace Digitronik.API.Security.Application.Mapping;

public class RequestToModel : Profile
{
    public RequestToModel()
    {
        CreateMap<RegisterUserCommand, User>();
    }
}