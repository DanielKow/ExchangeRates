using ExchangeRatesSource.ApplicationLayer.Services;
using ExchangeRatesSource.DomainLayer;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRatesSource.PresentationLayer.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ExchangeRatesController : ControllerBase
{
    private readonly IGetExchangeRatesService _getExchangeRatesService;

    public ExchangeRatesController(IGetExchangeRatesService getExchangeRatesService)
    {
        _getExchangeRatesService = getExchangeRatesService;
    }

    [HttpGet(Name = "GetExchangeRates")]
    public async Task<IEnumerable<ExchangeRate>> Get()
    {
        return await _getExchangeRatesService.GetAll();
    }
}