using Common.Entities;
using Common.Interfaces;

namespace ExchangeService.Services;
class ExchangeService : IExchangeService
{
    private readonly IBaseRepository<ExchangeRate> _repository;
    public ExchangeService(IBaseRepository<ExchangeRate> repository)
    {
        _repository = repository;
    }

    public Task<ExchangeRate[]> GetManyAsync(string[] exchanges)
    {
        return _repository.GetMultipleAsync(exchanges);
    }

    public Task<ExchangeRate> GetAsync(string exchange)
    {
        return _repository.GetSingleAsync(exchange);
    }

    public Task<ExchangeRate[]> GetManyAsync(string[] exchanges, long ts)
    {
        throw new NotImplementedException();
    }

    public Task<ExchangeRate> GetSingleAsync(string exchange, long ts)
    {
        throw new NotImplementedException();
    }

    public Task<ExchangeRate[]> GetAllAsync(long ts)
    {
        throw new NotImplementedException();
    }
}