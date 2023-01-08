using System.Net;
using ExchangeService.Services;
using Common.Entities;
using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeService.Controllers;

[ApiController]
[Route("[controller]")]
public class ExchangeController : ControllerBase
{
    private readonly ILogger<ExchangeController> _logger;
    private readonly IExchangeService _exchangeService;
    private readonly IBaseRepository<ExchangeRate> _repository;

    public ExchangeController(ILogger<ExchangeController> logger, IBaseRepository<ExchangeRate> repository)
    {
        _logger = logger;
        _repository = repository;
        _exchangeService = new Services.ExchangeService(repository);
    }

    [Route("api/ExchangeRate/{name}")]
    [HttpGet]
    public async Task<ExchangeRate> GetAsync(string name)
    {
        return await _exchangeService.GetSingleAsync(name, ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds());
    }
    [Route("api/ExchangeRate")]
    [HttpPost]
    public async Task<ExchangeRate[]> GetManyAsync(string[] exchanges)
    {
        return await _exchangeService.GetManyAsync(exchanges, ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds());
    }
    [Route("api/ExchangeRate")]
    [HttpGet]
    public async Task<ExchangeRate[]> GetAllAsync()
    {
        return await _exchangeService.GetAllAsync(((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds());
    }
}
