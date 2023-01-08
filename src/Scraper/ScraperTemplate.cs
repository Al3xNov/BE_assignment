namespace Scraper;
public class ScraperTemplate
{
    public string WrapperPattern { get; set; }
    public string TargetKeyPattern { get; set; }
    public string TargetValuePattern { get; set; }
    public string TargetKeyAttribute { get; set; }
    public string TargetValueAttribute { get; set; }
    public string[]? TargetFilter { get; set; }
    public string[] Urls { get; set; }

    public ScraperTemplate(string[] Urls, string WrapperPattern, string TargetValuePattern, string TargetKeyPattern, string TargetKeyAttribute, string TargetValueAttribute, string[]? TargetFilter)
    {
        this.Urls = Urls;
        this.WrapperPattern = WrapperPattern;
        this.TargetKeyPattern = TargetKeyPattern;
        this.TargetKeyAttribute = TargetKeyAttribute;
        this.TargetValuePattern = TargetValuePattern;
        this.TargetValueAttribute = TargetValueAttribute;
        this.TargetFilter = TargetFilter;
    }

}