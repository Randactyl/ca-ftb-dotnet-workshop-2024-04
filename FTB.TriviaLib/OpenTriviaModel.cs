using System.Collections.Generic;
using Newtonsoft.Json;

namespace FTB.TriviaLib
{
    public class OpenTriviaModel
    {
        [JsonProperty("response_code")]
        public long ResponseCode { get; set; }

        [JsonProperty("results")]
        public List<Question> Results { get; set; } = new List<Question>();
    }
}
