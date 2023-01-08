using Microsoft.AspNetCore.Mvc;
using Common.Entities;
using Scraper;
using Common.Interfaces;
using ScraperService.Dataflow;

namespace ScraperService.Controllers;

[ApiController]
// [Route("[controller]")]
public class ScraperExchangeController : ControllerBase
{
    private readonly ILogger<ScraperExchangeController> _logger;
    private readonly IMongoBaseRepository<ExchangeRateDoc> _exchangeRepository;
    public ScraperExchangeController(ILogger<ScraperExchangeController> logger, IMongoBaseRepository<ExchangeRateDoc> exchangeRepository)
    {
        _logger = logger;
        _exchangeRepository = exchangeRepository;
    }

    [Route("api/ExchangeScraper")]
    [HttpPost]
    public async Task FetchAndSaveExchanges([FromBody] ScraperTemplate template)
    {
        Console.WriteLine("Entered Controller Fetch and Save");
        var dataflow = new DataFlowExchangeScraper(template, _exchangeRepository);
        Task<bool>[] urlStart = new Task<bool>[template.Urls.Length];
        for (var i = 0; i < template.Urls.Length; i++)
        {
            urlStart[i] = dataflow.StartPipeline(template.Urls[i]);
        }
        var started = await Task.WhenAll(urlStart);
        // second try
        for (var i = 0; i < template.Urls.Length; i++)
        {
            if (!started[i]) await dataflow.StartPipeline(template.Urls[i]);
        }
        dataflow.DoneProducing();
        await dataflow.DoneProcessing();
    }
}
