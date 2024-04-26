using System.Collections.Generic;
using Newtonsoft.Json;

namespace FTB.TriviaLib
{
    public class CategoryModel
    {
        [JsonProperty("trivia_categories")]
        public List<TriviaCategory> TriviaCategories { get; set; } = new List<TriviaCategory>();
    }

    public class TriviaCategory
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }
}