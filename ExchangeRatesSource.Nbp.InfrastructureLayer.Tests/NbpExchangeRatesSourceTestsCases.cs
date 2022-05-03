using System.Collections;
using System.Collections.Generic;
using System.Net;
using ExchangeRatesSource.Nbp.DomainLayer;
using NUnit.Framework;

namespace ExchangeRatesSource.Nbp.InfrastructureLayer.Tests;

[Category("test_cases")]
internal class NbpExchangeRatesSourceTestsCases : NbpExchangeRatesSourceTestsBase
{
    protected static IEnumerable<HttpStatusCode> SomeOfNotOkStatusCodes => new[]
    {
        HttpStatusCode.Forbidden,
        HttpStatusCode.BadRequest,
        HttpStatusCode.NotFound,
        HttpStatusCode.InternalServerError
    };

    protected static IEnumerable<string> NotAcceptedExchangeRatesTypes => new[]
    {
        NbpExchangeRatesTableType.C.Value,
        "test",
        "abc",
        "AlaMaKota"
    };
}