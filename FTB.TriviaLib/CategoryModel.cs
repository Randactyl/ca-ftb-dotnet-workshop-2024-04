namespace FTB.TriviaLib
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public partial class CategoryModel
    {
        [JsonProperty("trivia_categories")]
        public List<TriviaCategory> TriviaCategories { get; set; }
    }

    public partial class TriviaCategory
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}