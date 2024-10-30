using ByteBank.API.Security.Application.Features;
using ByteBank.API.Security.Application.Mapping;
using ByteBank.API.Security.Domain.Repositories;
using ByteBank.API.Security.Domain.Services;
using ByteBank.API.Security.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ByteBank.API.Security;

public static class SecurityRegistration
{
    public static IServiceCollection AddSecurityServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        
        services.AddAutoMapper(
            typeof(RequestToModel),
            typeof(ModelToResponse)
        );

        services.AddScoped<IEncryptService, EncryptService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserCommandService, UserCommandService>();
        services.AddScoped<IUserQueryService, UserQueryService>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
        
        return services;
    }
}