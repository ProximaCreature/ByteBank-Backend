using ByteBank.API.Wallet.Applications.Features;
using ByteBank.API.Wallet.Applications.Mappings;
using ByteBank.API.Wallet.Domain.Repository;
using ByteBank.API.Wallet.Domain.Service;
using ByteBank.API.Wallet.Infrastructures.Persistence.Repositories;

namespace ByteBank.API.Wallet;

public static class WalletRegistration
{
    public static IServiceCollection AddWalletService(this IServiceCollection services)
    {
        services.AddScoped<IWalletRepository, WalletRepository>();
        
        services.AddAutoMapper(
            typeof(RequestToModel),
            typeof(ModelToResponse)
        );

        services.AddScoped<IWalletCommandService, WalletCommandService>();
        services.AddScoped<IWalletQueryService, WalletQueryService>();
        
        return services;
    }
}