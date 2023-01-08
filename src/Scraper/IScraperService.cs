namespace Scraper;
public interface IScraperService<T>
{
    Task<IEnumerable<T>> Scrape(string url, ScraperTemplate template);
    Task<bool> SaveScraped(T[] entity);
}