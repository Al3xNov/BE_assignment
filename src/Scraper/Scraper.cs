
using HtmlAgilityPack;

namespace Scraper;
public static class Scraper
{
    public static async Task<HtmlDocument> ScrapeHtml(string url)
    {
        return await new HtmlWeb().LoadFromWebAsync(url);
    }

    // descendant-or-self //
    // find via inner HTML use .=''
    // fiva via attribute use @attName <operator> value/attName2 
    // Comparison operators =, !=, <, >, <=, >=
    // //table/tbody
    // /tr/td [aria-label="Last Price"]
    // public async Dictionary<string, string> ScrapeValues(HtmlDocument htmlDoc, string wrapperPattern, (string, string?) descendantsPatternAndValueAttribute)
    // { }

    public static HtmlNodeCollection ExtractWrapperNodes(HtmlDocument htmlDoc, string pattern)
    {
        return htmlDoc.DocumentNode.SelectNodes(pattern);
    }

    public static HtmlNode? ExtractValueNode(HtmlNode node, string pattern)
    {
        return node.SelectSingleNode(pattern);
    }

    public static string? ExtractValue(HtmlNode node, string? attribute)
    {
        return !string.IsNullOrEmpty(attribute) ? node.GetAttributeValue(attribute, null) : node.InnerText;
    }
    public static async Task<IDictionary<string, string>> ScrapeKeyValue(HtmlDocument htmlDoc, ScraperTemplate scrpTemplate)
    {
        var scrapedData = new Dictionary<string, string>();
        foreach (var wrapper in htmlDoc.DocumentNode.SelectNodes(scrpTemplate.WrapperPattern))
        {
            // Console.WriteLine($" ")
            var nodeKey = wrapper.SelectSingleNode(scrpTemplate.TargetKeyPattern);
            var key = string.IsNullOrEmpty(scrpTemplate.TargetKeyAttribute) ? nodeKey.GetAttributeValue("textContents", null) : nodeKey.GetAttributes().FirstOrDefault(att => att.Name.Equals(scrpTemplate.TargetKeyAttribute))?.Value;
            if (key is not null)
            {
                var nodeValue = wrapper.SelectSingleNode(scrpTemplate.TargetValuePattern);
                var value = string.IsNullOrEmpty(scrpTemplate.TargetValueAttribute) ? nodeValue.GetAttributeValue("textContents", null) : nodeValue.GetAttributes().FirstOrDefault(att => att.Name.Equals(scrpTemplate.TargetValueAttribute))?.Value;
                if (value is not null) { scrapedData.Add(key, value); }
                else { Console.WriteLine($"Key {key} Found, Value Not Found..."); }
            }
            else { Console.WriteLine($"Key {key} Not Found"); }
        }
        return scrapedData;
    }
}
