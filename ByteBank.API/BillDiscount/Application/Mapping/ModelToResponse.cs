using AutoMapper;
using ByteBank.API.BillDiscount.Domain.Models.Aggregates;
using ByteBank.API.BillDiscount.Domain.Models.Responses;

namespace ByteBank.API.BillDiscount.Application.Mapping;

public class ModelToResponse : Profile
{
    public ModelToResponse()
    {
        CreateMap<Bill, BillResponse>();
    }
}