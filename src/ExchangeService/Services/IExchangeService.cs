using Common.Entities;

namespace ExchangeService.Services;
interface IExchangeService
{
    Task<ExchangeRate[]> GetManyAsync(string[] exchanges, long ts);
    Task<ExchangeRate> GetSingleAsync(string exchange, long ts);
    Task<ExchangeRate[]> GetAllAsync(long ts);
}