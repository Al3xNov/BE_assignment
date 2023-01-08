using MongoDB.Bson;

namespace Common.Entities;
public class ExchangeRateDoc : ExchangeRate
{
    public ObjectId? Id { get; set; }
    public ExchangeRateDoc(string Name, string Rate) : base(Name, Rate) { }
    public ExchangeRateDoc(ObjectId Id, string Name, string Rate) : base(Name, Rate)
    {
        this.Id = Id;
    }
    public ExchangeRateDoc(string Name, string Rate, long? Timestamp = null) : base(Name, Rate, Timestamp) { }
    public ExchangeRateDoc(ObjectId Id, string Name, string Rate, long? Timestamp = null) : base(Name, Rate, Timestamp)
    {
        this.Id = Id;
    }
}