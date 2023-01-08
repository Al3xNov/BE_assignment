// using Common.Entities;
// using Common.Interfaces;
// using HtmlAgilityPack;
// using Scraper;

// namespace ScraperService.Services;
// public class ExchangeScraperService : IScraperService<ExchangeRate>
// {
//     private readonly IBaseRepository<ExchangeRate> _repository;
//     public ExchangeScraperService(IBaseRepository<ExchangeRate> repository)
//     {
//         _repository = repository;
//     }

//     public async Task<IEnumerable<ExchangeRate>> Scrape(string url, ScraperTemplate template)
//     {
//         HtmlDocument htmlDoc = await Scraper.Scraper.ScrapeHtml(url);
//         return (await Scraper.Scraper.ScrapeKeyValue(htmlDoc, template)).Select(d => new ExchangeRate(d.Key, d.Value));
//     }

//     public async Task SaveScraped(ExchangeRate[] exchanges)
//     {
//         await _repository.SaveMultipleAsync(exchanges);
//     }
// }