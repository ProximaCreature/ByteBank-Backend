using ByteBank.API.BillDiscount.Domain.Models.Aggregates;
using ByteBank.API.Security.Domain.Models.Aggregates;
using ByteBank.API.Shared.Domain.Models.Entities;
using ByteBank.API.Wallet.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace ByteBank.API.Shared.Infrastructure.Persistence.Configuration;

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
        
        builder.Entity<Bill>().Property(s => s.Id).ValueGeneratedOnAdd();
        builder.Entity<Bill>().Property(s => s.Currency).HasConversion<string>();
        builder.Entity<Bill>().Property(s => s.ExpirationDate).HasColumnType("date");
        
        builder.Entity<Wallets>().Property(s => s.Id).ValueGeneratedOnAdd();
        builder.Entity<Wallets>().Property(s => s.NombreCartera).IsRequired().HasMaxLength(30);
        
        builder.Entity<Wallets>()
            .HasMany(w => w.Bills)
            .WithOne(b => b.Wallets)
            .HasForeignKey(b => b.WalletId);
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<Wallets> Wallets { get; set; }
}