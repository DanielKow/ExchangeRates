using ExchangeRatesSource.ApplicationLayer;
using ExchangeRatesSource.ApplicationLayer.Services;
using ExchangeRatesSource.InfrastructureLayer.Services;
using ExchangeRatesSource.Nbp.InfrastructureLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using WebApi.ApplicationLayer.GetExchangeRatesChain;
using WebApi.DomainLayer.Config;
using WebApi.InfrastructureLayer.GetExchangeRatesChain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IExternalSourcesConfig, ExternalSourcesConfig>();
builder.Services.AddScoped<IInternalSourcesConfig, InternalSourcesConfig>();
builder.Services.AddScoped<IExchangeRatesSource, NbpExchangeRatesSource>();
builder.Services.AddScoped<IGetExchangeRatesChainBuilder, GetExchangeRatesChainBuilder>();
builder.Services.AddScoped<IGetExchangeRatesService, GetExchangeRatesService>();

builder.Services.AddHttpClient<GetExchangeRatesChainBuilder>();

builder.Services.AddControllers()
    .AddXmlSerializerFormatters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = new HeaderApiVersionReader("X-Version");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();