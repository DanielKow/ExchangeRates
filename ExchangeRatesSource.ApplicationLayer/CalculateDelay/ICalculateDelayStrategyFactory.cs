namespace ExchangeRatesSource.ApplicationLayer.CalculateDelay;

public interface ICalculateDelayStrategyFactory
{
    ICalculateDelayStrategy GetStrategyForType(string type);
}