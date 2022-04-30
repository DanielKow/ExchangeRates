using ExchangeRatesSource.DomainLayer;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRatesSource.InfrastructureLayer.Db;

public class ExchangeRateContext : DbContext
{
    public DbSet<ExchangeRate> ExchangeRates { get; set; }


    public ExchangeRateContext(DbContextOptions<ExchangeRateContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExchangeRate>()
            .HasKey(row => row.Currency);
    }
}