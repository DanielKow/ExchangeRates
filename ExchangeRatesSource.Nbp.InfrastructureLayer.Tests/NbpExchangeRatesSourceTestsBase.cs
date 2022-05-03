using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ExchangeRatesSource.DomainLayer;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace ExchangeRatesSource.Nbp.InfrastructureLayer.Tests;

[Category("base")]
internal class NbpExchangeRatesSourceTestsBase : NbpExchangeRatesSourceTestsData
{
    private Mock<HttpMessageHandler> _httpMessageHandlerMock = null!;
    private Mock<ILogger<NbpExchangeRatesSource>> _loggerMock = null!;

    [SetUp]
    public void SetUp()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _loggerMock = new Mock<ILogger<NbpExchangeRatesSource>>();
    }

    protected void SetUpResponseWithExchangeRatesTableFromNbp(string content)
    {
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content)
            });
    }

    protected void SetUpNotOkStatusOfGettingExchangeRates(HttpStatusCode code)
    {
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
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
        var httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        httpClient.BaseAddress = new Uri("https://test.com");
        return new NbpExchangeRatesSource(httpClient, _loggerMock.Object);
    }
}