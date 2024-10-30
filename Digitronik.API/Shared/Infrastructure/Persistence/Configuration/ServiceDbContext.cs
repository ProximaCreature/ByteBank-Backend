using Digitronik.API.Security.Domain.Models.Aggregates;
using Digitronik.API.Shared.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Digitronik.API.Shared.Infrastructure.Persistence.Configuration;

public class ServiceDbContext : DbContext
{
    public ServiceDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.Now;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
     
        builder.Entity<User>().Property(s => s.Id).ValueGeneratedOnAdd();
        builder.Entity<User>().Property(s => s.Username).IsRequired().HasMaxLength(30);
        builder.Entity<User>().Property(s => s.Password).IsRequired().HasMaxLength(120);
    }
    
    public DbSet<User> Users { get; set; }
}