using AutoMapper;
using ByteBank.API.BillDiscount.Domain.Models.Aggregates;
using ByteBank.API.BillDiscount.Domain.Models.Commands;
using ByteBank.API.BillDiscount.Domain.Models.ValueObjects;

namespace ByteBank.API.BillDiscount.Application.Mapping;

public class RequestToModel : Profile
{
    public RequestToModel()
    {
        CreateMap<CreateBillCommand, Bill>()
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => Enum.Parse<Currency>(src.Currency, true)));
    }
}