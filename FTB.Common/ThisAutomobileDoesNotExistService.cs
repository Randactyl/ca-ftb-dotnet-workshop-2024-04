using HtmlAgilityPack;

namespace FTB.Common;

public class ThisAutomobileDoesNotExistService
{
    private const string ServiceUrl = "https://www.thisautomobiledoesnotexist.com/";

    private readonly HttpClient httpClient = new();

    private readonly HtmlDocument document = new();

    private const string AutomobileNamesFilePath = "automobileNames.txt";

    private readonly List<string> automobileNames = new();

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

    public string GetRandomAutomobileName()
    {
        if (this.automobileNames.Any())
        {
            this.automobileNames.Shuffle();
        }
        else
        {
            IEnumerable<string> automobileNamesFromFile = File.ReadLines(AutomobileNamesFilePath);
            foreach (string automobileNameFromFile in automobileNamesFromFile)
            {
                this.automobileNames.Add(automobileNameFromFile);
            }
        }

        int firstName = Random.Shared.Next(this.automobileNames.Count);
        if (this.automobileNames[firstName].Contains(' '))
        {
            string randomAutomobileName = this.automobileNames[firstName];
            this.automobileNames.RemoveAt(firstName);
            return randomAutomobileName;
        }

        int secondName = Random.Shared.Next(this.automobileNames.Count);
        if (this.automobileNames[secondName].Contains(' '))
        {
            string randomAutomobileName = this.automobileNames[secondName];
            this.automobileNames.RemoveAt(secondName);
            return randomAutomobileName;
        }

        string generatedAutomobileName = $"{this.automobileNames[firstName]} {this.automobileNames[secondName]}";
        return generatedAutomobileName;
    }

}
