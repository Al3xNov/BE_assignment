using Common.Entities;
using MongoDB.Driver;

namespace Common.Repositories;
public class ExchangeRateMongoRepository : MongoBaseRepository<ExchangeRateDoc>
{
    public ExchangeRateMongoRepository(IMongoCollection<ExchangeRateDoc> collection) : base(collection)
    {
    }

    new public async Task<List<ExchangeRateDoc>> GetAllAsync(long ts)
    {
        var options = new FindOptions<ExchangeRateDoc, ExchangeRateDoc>() { Sort = Builders<ExchangeRateDoc>.Sort.Descending("Timestamp"), Limit = 1 };
        return await (await _collection.FindAsync(x => x.Timestamp <= ts, options)).ToListAsync();
    }
}
