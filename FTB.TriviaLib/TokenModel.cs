using Newtonsoft.Json;

namespace FTB.TriviaLib
{
    public class TokenModel
    {
        [JsonProperty("response_code")]
        public long ResponseCode { get; set; }

        [JsonProperty("response_message")]
        public string ResponseMessage { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}

