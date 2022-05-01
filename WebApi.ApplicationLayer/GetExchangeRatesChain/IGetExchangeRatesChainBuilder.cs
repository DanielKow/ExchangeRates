namespace WebApi.ApplicationLayer.GetExchangeRatesChain;

public interface IGetExchangeRatesChainBuilder
{
    IGetExchangeRatesChainLink BuildChain();
}