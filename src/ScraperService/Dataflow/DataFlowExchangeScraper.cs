using System.Threading.Tasks.Dataflow;
using Common.Entities;
using Common.Interfaces;
using HtmlAgilityPack;
using Scraper;

namespace ScraperService.Dataflow;
public class DataFlowExchangeScraper : IDataflow
{
    public DataFlowExchangeScraper(ScraperTemplate template, IBaseRepository<ExchangeRateDoc> repository)
    {
        var fakeDoc = new HtmlDocument();
        fakeDoc.LoadHtml("<html></html>");
        DownloadHtml = new(async url =>
        {
            Console.WriteLine($"DownloadHtml {url}");
            var timestamp = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();
            var html = await Scraper.Scraper.ScrapeHtml(url) ?? fakeDoc;
            return (timestamp, html);
        });

        ExtractWrapper = new(tuple => Scraper.Scraper.ExtractWrapperNodes(tuple.Item2, template.WrapperPattern)?.Select(dataNode => (tuple.Item1, dataNode)) ?? new List<(long, HtmlNode)>() { (tuple.Item1, new HtmlNode(HtmlNodeType.Document, fakeDoc, 0)) });

        ExtractData = new(tuple =>
        {
            Console.WriteLine($"ExtractData {tuple.Item2.XPath}");
            HtmlNode? keyN = Scraper.Scraper.ExtractValueNode(tuple.Item2, template.TargetKeyPattern);
            HtmlNode? valN = Scraper.Scraper.ExtractValueNode(tuple.Item2, template.TargetValuePattern);
            string? keyV = keyN is not null ? Scraper.Scraper.ExtractValue(keyN, template.TargetKeyAttribute) : null;
            string? valV = valN is not null ? Scraper.Scraper.ExtractValue(valN, template.TargetValueAttribute) : null;
            if (template.TargetFilter?.Length > 0)
            {
                return template.TargetFilter.Contains(keyV) ? (tuple.Item1, keyV, valV) : (tuple.Item1, null, null);
            }
            return (tuple.Item1, keyV, valV);
        });

        SaveResult = new(async result =>
        {
            Console.WriteLine($"Result {result} to Save?");
            if (!string.IsNullOrEmpty(result.Item2) && !string.IsNullOrEmpty(result.Item3))
            {
                try
                {
                    await repository.SaveSingleAsyncFromTuples(new ExchangeRateDoc(result.Item2, result.Item3, result.Item1));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"handle error: {e.Message}");
                }
            }
        });

        var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
        DownloadHtml.LinkTo(ExtractWrapper, linkOptions);
        ExtractWrapper.LinkTo(ExtractData, linkOptions);
        ExtractData.LinkTo(SaveResult, linkOptions);
    }

    public async Task<bool> StartPipeline(object url)
    {
        Console.WriteLine($"Starting Pipeline for {url}");
        return url is not null && url is string @string && await DownloadHtml.SendAsync(@string);
    }
    public void DoneProducing()
    {
        Console.WriteLine("DoneProducing");
        DownloadHtml.Complete();
    }

    public async Task DoneProcessing()
    {
        await ExtractWrapper.Completion;
        Console.WriteLine("DoneProcessing ExtractWrapper");
        await ExtractData.Completion;
        Console.WriteLine("DoneProcessing ExtractData");
        await SaveResult.Completion;
        Console.WriteLine("DoneProcessing SaveResult");
    }
    // Get Html
    readonly TransformBlock<string, (long, HtmlDocument)> DownloadHtml;
    // Extract the wrapping node
    readonly TransformManyBlock<(long, HtmlDocument), (long, HtmlNode)> ExtractWrapper;
    // Extract values from nodes
    readonly TransformBlock<(long, HtmlNode), (long, string?, string?)> ExtractData;
    // Save Result
    readonly ActionBlock<(long, string?, string?)> SaveResult;
}