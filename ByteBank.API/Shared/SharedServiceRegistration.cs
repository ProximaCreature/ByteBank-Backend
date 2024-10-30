using ByteBank.API.Shared.Domain.Repositories;
using ByteBank.API.Shared.Infrastructure.Persistence.Configuration;
using ByteBank.API.Shared.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ByteBank.API.Shared;

public static class SharedServiceRegistration
{
    public static IServiceCollection AddSharedService(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ServiceConnection");

        services.AddDbContext<ServiceDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
      
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllPolicy",
                policy => policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        return services;
    }
}