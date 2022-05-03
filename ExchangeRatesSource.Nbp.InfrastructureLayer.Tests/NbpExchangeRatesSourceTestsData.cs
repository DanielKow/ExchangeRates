using System;
using System.Collections.Immutable;
using System.Text.Json.Nodes;
using ExchangeRatesSource.DomainLayer;
using NUnit.Framework;

namespace ExchangeRatesSource.Nbp.InfrastructureLayer.Tests;

[Category("test_data")]
internal class NbpExchangeRatesSourceTestsData
{
    protected string TestIncorrectResponse =>
        new JsonObject
        {
            ["test"] = "test1",
            ["this_is"] = "not_correct"
        }.ToString();

    protected string TestCorrectResponseWithExchangeRatesTable =>
        new JsonArray
        {
            new JsonObject
            {
                ["table"] = "A",
                ["no"] = "084/A/NBP/2022",
                ["effectiveDate"] = "2022-05-02",
                ["rates"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["currency"] = "bat (Tajlandia)",
                        ["code"] = "THB",
                        ["mid"] = 0.1293
                    },
                    new JsonObject
                    {
                        ["currency"] = "dolar amerykański",
                        ["code"] = "USD",
                        ["mid"] = 4.4454
                    },
                    new JsonObject
                    {
                        ["currency"] = "euro",
                        ["code"] = "EUR",
                        ["mid"] = 4.6806
                    }
                }
            }
        }.ToString();

    protected GettingExchangeRatesResult TestGettingExchangeRatesResult => new GettingExchangeRatesResult(
        new DateOnly(2022, 5, 2),
        new ImmutableArray<ExchangeRate>
        {
            new ExchangeRate("THB", 0.1293m),
            new ExchangeRate("USD", 4.4454m),
            new ExchangeRate("EUR", 4.6806m)
        });
}