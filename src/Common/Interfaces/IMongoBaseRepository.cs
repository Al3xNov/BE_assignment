using Common.Entities;

namespace Common.Interfaces;
public interface IMongoBaseRepository<T> : IBaseRepository<T>
{
    Task<List<T>> GetAllAsync(long ts);
}