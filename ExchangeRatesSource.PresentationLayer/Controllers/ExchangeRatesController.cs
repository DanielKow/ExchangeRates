using Microsoft.AspNetCore.Mvc;

namespace ExchangeRatesSource.PresentationLayer.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
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