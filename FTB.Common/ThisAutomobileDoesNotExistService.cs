using HtmlAgilityPack;

namespace FTB.Common;

public class ThisAutomobileDoesNotExistService
{
    private const string ServiceUrl = "https://www.thisautomobiledoesnotexist.com/";

    private readonly HttpClient httpClient = new();

    private readonly HtmlDocument document = new();

    public async Task<string> GetRandomAutomobileImageAsync()
    {
        string automobileDoesNotExistHtml = await this.httpClient.GetStringAsync(ServiceUrl);
        if (string.IsNullOrEmpty(automobileDoesNotExistHtml))
        {
            throw new Exception("website down!");
        }

        this.document.LoadHtml(automobileDoesNotExistHtml);

        // Check if the image exists
        HtmlNode imgNode = this.document.DocumentNode.SelectSingleNode("//img[@id='vehicle']") ?? throw new Exception("website down!");
        string src = imgNode.GetAttributeValue("src", "");
        return src;
    }
}
