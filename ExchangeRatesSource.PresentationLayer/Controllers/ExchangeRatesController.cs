using Microsoft.AspNetCore.Mvc;

namespace ExchangeRatesSource.PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExchangeRatesController : ControllerBase
{
    [HttpGet(Name = "GetExchangeRates")]
    public IEnumerable<string> Get()
    {
        return new[]
        {
            "test"
        };
    }
}