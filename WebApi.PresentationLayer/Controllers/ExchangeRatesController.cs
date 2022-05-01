using ExchangeRatesSource.DomainLayer;
using Microsoft.AspNetCore.Mvc;
using WebApi.ApplicationLayer;

namespace WebApi.PresentationLayer.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
public class ExchangeRatesController : ControllerBase
{
    private readonly IExchangeRatesService _exchangeRatesService;

    public ExchangeRatesController(IExchangeRatesService exchangeRatesService)
    {
        _exchangeRatesService = exchangeRatesService;
    }

    [HttpGet(Name = "GetAllExchangeRates")]
    public async Task<IEnumerable<ExchangeRate>> Get()
    {
        return await _exchangeRatesService.GetExchangeRates();
    }
}