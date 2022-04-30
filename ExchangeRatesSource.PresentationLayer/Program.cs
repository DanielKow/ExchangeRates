using ExchangeRatesSource.ApplicationLayer;
using ExchangeRatesSource.ApplicationLayer.CalculateDelay;
using ExchangeRatesSource.DomainLayer;
using ExchangeRatesSource.DomainLayer.Time;
using ExchangeRatesSource.InfrastructureLayer.Cache;
using ExchangeRatesSource.InfrastructureLayer.CalculateDelay;
using ExchangeRatesSource.InfrastructureLayer.Db;
using ExchangeRatesSource.InfrastructureLayer.Nbp;
using ExchangeRatesSource.InfrastructureLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddScoped<ITimeProvider, TimeProvider>();
builder.Services.AddScoped<ICalculateDelayStrategyFactory, NbpCalculateDelayStrategyFactory>();
builder.Services.AddScoped<IExchangeRatesSource, NbpExchangeRatesSource>();
builder.Services.AddScoped<IGenericRepository<ExchangeRate>, GenericRepository<ExchangeRate>>();
builder.Services.AddScoped<ILastUpdateDateCache, LastUpdateDateCache>();
builder.Services.AddScoped<IExchangeRatesUnitOfWork, ExchangeRatesUnitOfWork>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = configuration.GetConnectionString("Redis");
});

builder.Services.AddHttpClient<IExchangeRatesSource, NbpExchangeRatesSource>(client =>
{
    client.BaseAddress = new Uri(configuration["NbpUrl"]);
});

builder.Services.AddDbContext<ExchangeRateContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("Db"));
});

builder.Services.AddControllers()
    .AddXmlSerializerFormatters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddHostedService<UpdateExchangeRatesService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();