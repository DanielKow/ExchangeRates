using ExchangeRatesSource.InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ExchangeRateContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("db"), 
        b => b.MigrationsAssembly("Migrator"));
});

var app = builder.Build();

Console.WriteLine("Migration started.");

using var serviceScope = app.Services.CreateScope();
var context = serviceScope.ServiceProvider.GetRequiredService<ExchangeRateContext>();
context.Database.Migrate();

Console.WriteLine("Migration applied.");