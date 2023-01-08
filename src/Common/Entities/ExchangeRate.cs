namespace Common.Entities;
public class ExchangeRate
{
    public string Name { get; set; }
    public string Rate { get; set; }
    public long? Timestamp { get; set; }
    public ExchangeRate(string Name, string Rate)
    {
        this.Name = Name;
        this.Rate = Rate;
    }
    public ExchangeRate(string Name, string Rate, long? Timestamp = null) : this(Name, Rate)
    {
        this.Timestamp = Timestamp;
    }
}