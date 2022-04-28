using ExchangeRatesSource.DomainLayer;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRatesSource.InfrastructureLayer.Db;

public class ExchangeRateContext : DbContext
{
    public DbSet<ExchangeRateType> Types { get; set; }


    public ExchangeRateContext(DbContextOptions<ExchangeRateContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExchangeRateType>()
            .HasKey(row => row.Name);
    }
}