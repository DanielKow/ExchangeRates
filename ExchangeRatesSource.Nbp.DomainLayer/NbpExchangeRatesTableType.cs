namespace ExchangeRatesSource.Nbp.DomainLayer;

public readonly struct NbpExchangeRatesTableType
{
    public string Value { get; }
    
    private NbpExchangeRatesTableType(string value)
    {
        Value = value;
    }

    public static NbpExchangeRatesTableType A => new("A");
    public static NbpExchangeRatesTableType B => new("B");
    public static NbpExchangeRatesTableType C => new("C");
}