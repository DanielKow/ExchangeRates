using Microsoft.AspNetCore.Mvc;

namespace WebApi.PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExchangeRatesController : ControllerBase
{
    [HttpGet(Name = "GetAllExchangeRates")]
    public IEnumerable<string> Get()
    {
        return new[] {"test"};
    }
}