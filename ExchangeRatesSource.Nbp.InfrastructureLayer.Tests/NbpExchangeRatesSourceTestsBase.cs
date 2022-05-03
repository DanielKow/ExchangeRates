using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace ExchangeRatesSource.Nbp.InfrastructureLayer.Tests;

[Category("base")]
internal class NbpExchangeRatesSourceTestsBase
{
    private Mock<HttpClient> _httpClientMock = null!;
    private Mock<ILogger<NbpExchangeRatesSource>> _loggerMock = null!;

    [SetUp]
    public void SetUp()
    {
        _httpClientMock = new Mock<HttpClient>();
        _loggerMock = new Mock<ILogger<NbpExchangeRatesSource>>();
    }

    protected void SetUpResponseWithExchangeRatesTableFromNbp(string content)
    {
        _httpClientMock
            .Setup(moq => moq.GetAsync(It.IsAny<string?>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content)
            });
    }

    protected void SetUpNotBadRequestResponseFromNbp()
    {
        _httpClientMock
            .Setup(moq => moq.GetAsync(It.IsAny<string?>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            });
    }

    protected void SetUpInternalServerErrorResponseFromNbp()
    {
        _httpClientMock
            .Setup(moq => moq.GetAsync(It.IsAny<string?>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            });
    }
    
    protected NbpExchangeRatesSource GetMockedSource()
    {
        return new NbpExchangeRatesSource(_httpClientMock.Object, _loggerMock.Object);
    }
}