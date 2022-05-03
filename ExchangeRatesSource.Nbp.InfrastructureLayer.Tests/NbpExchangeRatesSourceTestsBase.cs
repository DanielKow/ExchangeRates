using System.Net;
using System.Net.Http;
using ExchangeRatesSource.DomainLayer;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace ExchangeRatesSource.Nbp.InfrastructureLayer.Tests;

[Category("base")]
internal class NbpExchangeRatesSourceTestsBase : NbpExchangeRatesSourceTestsData
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

    protected void SetUpNotOkStatusOfGettingExchangeRates(HttpStatusCode code)
    {
        _httpClientMock
            .Setup(moq => moq.GetAsync(It.IsAny<string?>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = code
            });
    }

    protected void Assert_that_GettingExchangeRatesResult_is_empty(GettingExchangeRatesResult actual)
    {
        Assert.That(actual.Successfully, Is.False);
        Assert.That(actual.ExchangeRates, Is.Empty);
    }

    protected void Assert_that_GettingExchangeRatesResults_have_same_values(
        GettingExchangeRatesResult actual,
        GettingExchangeRatesResult expected)
    {
        Assert.That(actual.Successfully, Is.EqualTo(expected.Successfully));
        Assert.That(actual.ExchangeRates, Is.EqualTo(expected.ExchangeRates));
        Assert.That(actual.LastUpdateDate, Is.EqualTo(expected.LastUpdateDate));
    }

    protected NbpExchangeRatesSource GetMockedSource()
    {
        return new NbpExchangeRatesSource(_httpClientMock.Object, _loggerMock.Object);
    }
}