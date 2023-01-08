using Common.Interfaces;
using MongoDB.Driver;

namespace Common.Repositories;
public class MongoBaseRepository<T> : IMongoBaseRepository<T>
{
    protected readonly IMongoCollection<T> _collection;

    public MongoBaseRepository(IMongoCollection<T> collection)
    {
        _collection = collection;
    }

    public Task<List<T>> GetAllAsync(long ts)
    {
        throw new NotImplementedException();
    }

    public Task<T[]> GetMultipleAsync(string[] column)
    {
        Console.WriteLine("RETURN MULTIPLE");
        return null;
    }

    public Task<T> GetSingleAsync(string column)
    {
        Console.WriteLine("RETURN SIGLE");
        return null;
    }

    public async Task SaveMultipleAsync(T[] entities)
    {
        Console.WriteLine("SAVED MULTIPLE");
        await _collection.InsertManyAsync(entities);
    }

    public async Task SaveSingleAsyncFromTuples(T obj)
    {
        Console.WriteLine("SAVE SINGLE");
        await _collection.InsertOneAsync(obj);
    }
}
