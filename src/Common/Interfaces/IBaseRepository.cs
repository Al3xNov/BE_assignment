namespace Common.Interfaces;
public interface IBaseRepository<T>
{
    Task<T> GetSingleAsync(string column);
    Task<T[]> GetMultipleAsync(string[] column);
    Task SaveSingleAsyncFromTuples(T entity);
    Task SaveMultipleAsync(T[] entities);
}