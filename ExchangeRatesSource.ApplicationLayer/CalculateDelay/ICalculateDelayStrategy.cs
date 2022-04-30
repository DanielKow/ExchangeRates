namespace ExchangeRatesSource.ApplicationLayer.CalculateDelay;

public interface ICalculateDelayStrategy
{
    int CalculateDelay();
    bool CheckIfActual(DateOnly lastUpdateDate);
}