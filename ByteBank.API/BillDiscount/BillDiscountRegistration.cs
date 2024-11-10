using ByteBank.API.BillDiscount.Application.Features;
using ByteBank.API.BillDiscount.Application.Mapping;
using ByteBank.API.BillDiscount.Domain.Repositories;
using ByteBank.API.BillDiscount.Domain.Services;
using ByteBank.API.BillDiscount.Infrastructure.Persistence.Repositories;

namespace ByteBank.API.BillDiscount;

public static class BillDiscountRegistration
{
    public static IServiceCollection AddBillDiscountService(this IServiceCollection services)
    {
        services.AddScoped<IBillRepository, BillRepository>();
        
        services.AddAutoMapper(
            typeof(RequestToModel),
            typeof(ModelToResponse)
        );

        services.AddScoped<IBillCommandService, BillCommandService>();
        services.AddScoped<IBillQueryService, BillQueryService>();
        
        return services;
    }
}